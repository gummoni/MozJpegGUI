@echo off

if exist "%1\*" (

cd /d "%1"
mkdir "mozjpeg" > nul 2>&1
for %%A in (*.jpg) do (
echo %%A
"%~dp0cjpeg.exe" -optimize -quality 85 -outfile "mozjpeg\%%A" "%%A"

)

) else if exist "%1" (

cd /d "%~dp0"
mkdir "%~dp1mozjpeg\" > nul 2>&1
echo "%1"
"%~dp0cjpeg.exe" -optimize -quality 85 -outfile "%~dp1mozjpeg\%~nx1" "%1"

)


