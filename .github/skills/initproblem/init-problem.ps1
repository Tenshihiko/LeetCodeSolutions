param(
	[Parameter(Mandatory = $true)]
	[ValidatePattern('^\d+$')]
	[string]$ProblemNumber
)

$ErrorActionPreference = 'Stop'

$projectDir = Resolve-Path (Join-Path $PSScriptRoot '..\..\..\LeetCode')
$now = Get-Date
$year = $now.ToString('yyyy')
$month = $now.ToString('MM')
$name = "LeetCode$ProblemNumber"

$targetDir = Join-Path $projectDir (Join-Path $year $month)
if (-not (Test-Path $targetDir)) {
	New-Item -ItemType Directory -Path $targetDir -Force | Out-Null
	Write-Host "Created directory: $targetDir"
}

$existing = Get-ChildItem -Path $projectDir -Recurse -Filter "$name.cs" -File |
	Where-Object { $_.FullName -notmatch '\\(bin|obj)\\' }
if ($existing) {
	throw "File $name.cs already exists: $($existing[0].FullName)"
}

function Invoke-LeetCodeGraphQL([string]$query, [hashtable]$variables) {
    $body = @{ query = $query; variables = $variables } | ConvertTo-Json -Depth 5 -Compress
    $response = Invoke-RestMethod -Uri 'https://leetcode.com/graphql' -Method Post `
        -ContentType 'application/json' -Body $body -Headers @{ Referer = 'https://leetcode.com' }
    return $response.data
}

$problem = $null
try {
    $searchQuery = 'query($k:String){questionList(categorySlug:"",limit:20,skip:0,filters:{searchKeywords:$k}){data{questionFrontendId titleSlug}}}'
    $slug = (Invoke-LeetCodeGraphQL $searchQuery @{ k = $ProblemNumber }).questionList.data |
        Where-Object { $_.questionFrontendId -eq $ProblemNumber } |
        Select-Object -First 1 -ExpandProperty titleSlug

    if ($slug) {
        $questionQuery = 'query($s:String!){question(titleSlug:$s){questionFrontendId title titleSlug content exampleTestcases metaData codeSnippets{langSlug code}}}'
        $problem = (Invoke-LeetCodeGraphQL $questionQuery @{ s = $slug }).question
    } else {
        Write-Warning "Problem $ProblemNumber not found on LeetCode"
    }
} catch {
    Write-Warning "Failed to fetch problem data from LeetCode: $($_.Exception.Message)"
}

$templatePath = Join-Path $projectDir 'LeetCodeTemplate.cs'
$targetFile = Join-Path $targetDir "$name.cs"
$content = Get-Content -Path $templatePath -Raw
$content = $content -replace '(?m)^namespace\s+LeetCodeTemplate\s*;', "namespace $name;"

$snippet = $problem.codeSnippets | Where-Object { $_.langSlug -eq 'csharp' } | Select-Object -First 1 -ExpandProperty code
if ($snippet -and $snippet -match '(?m)^\s*(public [^\r\n]+\))\s*\{') {
    $signature = $Matches[1].Trim()
    $method = "    $signature`r`n    {`r`n        throw new NotImplementedException();`r`n    }`r`n"
    $content = $content -replace '(?m)(^public class Solution\r?\n\{\r?\n)', "`$1$method"
}
Set-Content -Path $targetFile -Value $content -NoNewline -Encoding utf8
Write-Host "Created file: $targetFile"

if ($problem) {
    $text = [System.Net.WebUtility]::HtmlDecode(($problem.content -replace '<[^>]+>', ''))
    $outputs = [regex]::Matches($text, 'Output:\s*(.+)') | ForEach-Object { $_.Groups[1].Value.Trim() }

    Write-Host ''
    Write-Host '=== PROBLEM DATA ==='
    [ordered]@{
        id               = $problem.questionFrontendId
        title            = $problem.title
        url              = "https://leetcode.com/problems/$($problem.titleSlug)/"
        csharpSnippet    = $snippet
        metaData         = $problem.metaData | ConvertFrom-Json
        exampleTestcases = $problem.exampleTestcases
        expectedOutputs  = @($outputs)
    } | ConvertTo-Json -Depth 6
}
