@echo off

powershell -NoProfile -ExecutionPolicy Bypass -Command "& '%~dp0\update-psake.ps1' %*; exit 0 "
exit /B %errorlevel%