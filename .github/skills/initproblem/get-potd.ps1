$ErrorActionPreference = 'Stop'

$body = '{"query":"query{activeDailyCodingChallengeQuestion{date question{questionFrontendId title}}}"}'
$daily = (Invoke-RestMethod -Uri 'https://leetcode.com/graphql' -Method Post `
	-ContentType 'application/json' -Body $body -Headers @{ Referer = 'https://leetcode.com' }
).data.activeDailyCodingChallengeQuestion

Write-Host "Problem of the day ($($daily.date)): $($daily.question.questionFrontendId). $($daily.question.title)"
Write-Output $daily.question.questionFrontendId
