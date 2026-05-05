@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"

echo ==========================================
echo  SVN AUTO UPDATE / ADD / COMMIT
echo ==========================================

REM Comprobar SVN
svn --version --quiet >nul 2>&1
if errorlevel 1 (
    echo ERROR: SVN no esta instalado o no esta en el PATH.
    pause
    exit /b 1
)

REM Comprobar que estamos en working copy SVN
svn info >nul 2>&1
if errorlevel 1 (
    echo ERROR: Esta carpeta no parece una working copy SVN.
    pause
    exit /b 1
)

REM Mensaje de commit
set MSG=%*
if "%MSG%"=="" (
    set MSG=[N8MUGIPASARELA-1] Ok
)

echo.
echo Mensaje commit:
echo %MSG%
echo.

echo Actualizando SVN...
svn update
if errorlevel 1 (
    echo.
    echo ERROR: Fallo svn update. Revisa conflictos.
    pause
    exit /b 1
)

echo.
echo Comprobando conflictos...
svn status | findstr /B "C" >nul
if not errorlevel 1 (
    echo ERROR: Hay conflictos SVN. Resuelvelos antes de hacer commit.
    svn status
    pause
    exit /b 1
)

echo.
echo Anadiendo ficheros nuevos respetando svn:ignore...
svn add --force . --depth infinity
if errorlevel 1 (
    echo.
    echo ERROR: Fallo svn add.
    pause
    exit /b 1
)

echo.
echo Estado actual:
svn status

echo.
echo Comprobando si hay cambios...
svn status | findstr /R "^[AMDR!?~]" >nul
if errorlevel 1 (
    echo No hay cambios para commitear.
    pause
    exit /b 0
)

echo.
echo Haciendo commit...
svn commit -m "%MSG%"
if errorlevel 1 (
    echo.
    echo ERROR: Fallo svn commit.
    pause
    exit /b 1
)

echo.
echo ==========================================
echo  OK - Commit SVN realizado
echo ==========================================
pause