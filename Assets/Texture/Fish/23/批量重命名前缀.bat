@echo off
set n=%cd%
:aaa
if not "%n:\=%" == "%n%" set "n=%n:*\=%" & goto aaa
for /f "delims=" %%a in ('dir /a-d/b *.png') do ren "%%a" "%n%_%%a"

