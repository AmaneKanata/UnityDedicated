@echo off
SETLOCAL

:: Define paths
set ROOT_DIR=%~dp0
set PROTO_PATH=%ROOT_DIR%src\ProtoFiles
set TEMPLATE_PATH=%ROOT_DIR%src\Templates
set OUTPUT_PATH_CLIENT=%ROOT_DIR%..\Client\Assets\Scripts\Network\Core\Generated
set OUTPUT_PATH_SERVER=%ROOT_DIR%..\Server\Assets\Scripts\Network\Core\Generated
set PROTOC=%ROOT_DIR%src\protoc.exe
set GENERATOR=%ROOT_DIR%src\Generator.exe

:: Ensure output directory exists
if not exist %OUTPUT_PATH_CLIENT% mkdir %OUTPUT_PATH_CLIENT%
if not exist %OUTPUT_PATH_SERVER% mkdir %OUTPUT_PATH_SERVER%

:: Compile .proto files to C# source code
echo Compiling .proto files to C#...
for %%f in (%PROTO_PATH%\*.proto) do (
    echo Processing %%~nxf
    %PROTOC% -I=%PROTO_PATH% --csharp_out=%OUTPUT_PATH_CLIENT% %%f
    %PROTOC% -I=%PROTO_PATH% --csharp_out=%OUTPUT_PATH_SERVER% %%f
)

:: Run the Packet Generator
echo Running Packet Generator...
%GENERATOR% --proto_path "%PROTO_PATH%" --template_path "%TEMPLATE_PATH%" --output_path "%OUTPUT_PATH_CLIENT%" --recv "S_" --send "C_"
%GENERATOR% --proto_path "%PROTO_PATH%" --template_path "%TEMPLATE_PATH%" --output_path "%OUTPUT_PATH_SERVER%" --recv "C_" --send "S_"

echo All tasks completed successfully.
ENDLOCAL
