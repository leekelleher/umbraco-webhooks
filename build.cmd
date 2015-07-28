ECHO off

C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe build\Cleanup.proj
C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe build\Webhooks4Umbraco.package.proj

@IF %ERRORLEVEL% NEQ 0 GOTO err
@EXIT /B 0
:err
@PAUSE
@EXIT /B 1