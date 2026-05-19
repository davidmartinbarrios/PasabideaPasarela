@echo off
setlocal enabledelayedexpansion

set "COMDIR=%~dp0"
set "LOG=%COMDIR%registro_com_%USERNAME%.log"

echo ================================================== >> "%LOG%"
echo Fecha: %DATE% %TIME% >> "%LOG%"
echo Usuario: %USERNAME% >> "%LOG%"
echo Equipo: %COMPUTERNAME% >> "%LOG%"
echo Carpeta COM: %COMDIR% >> "%LOG%"
echo ================================================== >> "%LOG%"

echo Registrando COM legacy PasarelaT0NET...
echo Log: %LOG%

call :RegisterIfNeeded "MSCAL.OCX"     "{8E27C92E-1264-101C-8A2F-040224009C02}"
call :RegisterIfNeeded "mscomct2.ocx"  "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}"
call :RegisterIfNeeded "mscomm32.ocx"  "{648A5603-2C6E-101B-82B6-000000000014}"
call :RegisterIfNeeded "MSFlxGrd.ocx"  "{5E9E78A0-531B-11CF-91F6-C2863C385E30}"
call :RegisterIfNeeded "Systray.ocx"   "{9E7C1E18-807C-11D2-8745-00C04F844793}"
call :RegisterIfNeeded "msstdfmt.dll"  "{6B263850-900B-11D0-9484-00A0C91110ED}"

call :CopyTlbIfNeeded "msdatsrc.tlb" "C:\Windows\SysWOW64\msdatsrc.tlb"

echo. >> "%LOG%"
echo Fin: %DATE% %TIME% >> "%LOG%"
echo ================================================== >> "%LOG%"

echo.
echo Proceso terminado. Revisa el log:
echo %LOG%
pause
exit /b


:RegisterIfNeeded
set "FILE=%~1"
set "GUID=%~2"
set "FULLPATH=%COMDIR%%FILE%"

echo. >> "%LOG%"
echo [%FILE%] GUID %GUID% >> "%LOG%"

if not exist "%FULLPATH%" (
    echo ERROR: No existe "%FULLPATH%" >> "%LOG%"
    echo ERROR: No existe "%FULLPATH%"
    exit /b
)

reg query "HKCR\TypeLib\%GUID%" >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo OK: Ya registrado en HKCR\TypeLib\%GUID% >> "%LOG%"
    echo OK: %FILE% ya registrado
    exit /b
)

reg query "HKCR\WOW6432Node\TypeLib\%GUID%" >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo OK: Ya registrado en HKCR\WOW6432Node\TypeLib\%GUID% >> "%LOG%"
    echo OK: %FILE% ya registrado x86
    exit /b
)

echo Registrando "%FULLPATH%"... >> "%LOG%"
echo Registrando %FILE%...

C:\Windows\SysWOW64\regsvr32.exe /s "%FULLPATH%"
if %ERRORLEVEL% EQU 0 (
    echo OK: Registrado correctamente "%FULLPATH%" >> "%LOG%"
    echo OK: %FILE% registrado
) else (
    echo ERROR: Fallo registrando "%FULLPATH%". Codigo %ERRORLEVEL% >> "%LOG%"
    echo ERROR: Fallo registrando %FILE%
)

exit /b


:CopyTlbIfNeeded
set "FILE=%~1"
set "DEST=%~2"
set "FULLPATH=%COMDIR%%FILE%"

echo. >> "%LOG%"
echo [%FILE%] Copia TLB >> "%LOG%"

if not exist "%FULLPATH%" (
    echo ERROR: No existe "%FULLPATH%" >> "%LOG%"
    echo ERROR: No existe "%FULLPATH%"
    exit /b
)

if exist "%DEST%" (
    echo OK: Ya existe "%DEST%" >> "%LOG%"
    echo OK: %FILE% ya existe en SysWOW64
    exit /b
)

copy "%FULLPATH%" "%DEST%" /Y >> "%LOG%" 2>&1

if %ERRORLEVEL% EQU 0 (
    echo OK: Copiado "%FULLPATH%" a "%DEST%" >> "%LOG%"
    echo OK: %FILE% copiado
) else (
    echo ERROR: Fallo copiando "%FULLPATH%" a "%DEST%". Codigo %ERRORLEVEL% >> "%LOG%"
    echo ERROR: Fallo copiando %FILE%
)

exit /b