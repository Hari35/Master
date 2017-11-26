Copy both Settled and UnSettled CSV to location D:\\CSV
UI is text driven only, please use 'settled' or 'unsettled' text to find the betting details
This is hosted under IIS and not with IISEXPRESS, please create the VD using the command mentioned

PowerShell ISE>> New-WebVirtualDirectory -Site "Default Web Site" -Name "BettingDetails" -PhysicalPath "{{solutionpath}}" 
