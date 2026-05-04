# PasarelaT0NET

Este repositorio contiene el código de una aplicación Windows Forms escrita en Visual Basic .NET que originalmente se creó para .NET Framework 2.0. Al abrir el proyecto con versiones modernas de Visual Studio (por ejemplo Visual Studio 2022) suelen aparecer errores de compilación relacionados con referencias faltantes a bibliotecas de compatibilidad y componentes COM heredados. Esta guía resume los pasos necesarios para poder compilar el proyecto en un entorno actualizado.

## Compatibilidad con .NET Framework

Visual Studio 2022 ya no incluye los ensamblados de destino para .NET Framework 2.0. Para poder resolver las referencias a `Microsoft.VisualBasic.Compatibility` y `Microsoft.VisualBasic.Compatibility.Data` es necesario retargetear el proyecto a una versión compatible del framework (por ejemplo .NET Framework 4.8):

1. Abra el archivo del proyecto (`PasarelaT0NET.vbproj`) en Visual Studio.
2. En **My Project → Compile → Advanced Compile Options** cambie **Target framework** a **.NET Framework 4.8** (o una versión instalada que incluya los ensamblados de compatibilidad).
3. Acepte el mensaje de retargeting para actualizar automáticamente el archivo de proyecto (`<TargetFrameworkVersion>`).
4. En **Project → Add Reference** seleccione **Assemblies → Extensions** y marque **Microsoft.VisualBasic.Compatibility** y **Microsoft.VisualBasic.Compatibility.Data**.

Con este ajuste Visual Studio volverá a resolver las referencias a los ensamblados de compatibilidad que el proyecto espera.【F:PasarelaT0NET.vbproj†L23-L68】

## Componentes COM heredados

El proyecto depende de varios componentes COM (ADODB, MSXML2, MSComCtl2, etc.) definidos en el archivo de proyecto mediante elementos `<COMReference>`. Cuando se abre el proyecto en un equipo nuevo, Visual Studio necesita encontrar y registrar estos componentes en el registro de Windows para generar los ensamblados de interoperabilidad correspondientes.【F:PasarelaT0NET.vbproj†L69-L137】 Para evitar los errores de importación (`The type library importer could not convert the signature...`):

1. Asegúrese de tener instalado el paquete de características **.NET Desktop Development** junto con los componentes heredados de Visual Basic 6.0 (sección **Individual components → SDKs, libraries, and frameworks → Visual Basic 6.0 compatibility**).
2. Instale el **Microsoft Data Access Components (MDAC) 2.8** o **Microsoft OLE DB Driver** para registrar `msado15.dll`, y ejecute `regsvr32` desde una consola de administrador de 32 bits:
   ```bat
   %SystemRoot%\SysWOW64\regsvr32.exe "%ProgramFiles(x86)%\Common Files\System\ado\msado15.dll"
   ```
3. Para los controles ActiveX (MSACAL, MSComCtl2, MSFlexGrid, etc.) instale los componentes correspondientes o copie los archivos OCX y regístrelos igualmente con `regsvr32` en su versión de 32 bits.
4. Mantenga la plataforma de compilación del proyecto en **x86** (como ya se especifica en la configuración) para coincidir con los componentes COM de 32 bits y evitar errores de ejecución.【F:PasarelaT0NET.vbproj†L34-L52】

Después de registrar correctamente los componentes, Visual Studio regenerará los ensamblados de interoperabilidad y se eliminarán los errores de marshaling.

## Recomendaciones adicionales

- Si necesita mantener compatibilidad con .NET Framework 2.0, utilice una versión anterior de Visual Studio (por ejemplo Visual Studio 2008/2010) que todavía incluya dicho framework.
- Considere migrar gradualmente la aplicación a APIs administradas modernas para reducir la dependencia de las bibliotecas de compatibilidad y los controles COM heredados.

Estos pasos deberían permitirle abrir y compilar el proyecto desde Visual Studio 2022 sin los errores de referencia descritos al inicio.
