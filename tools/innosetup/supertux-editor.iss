; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
AppName=SuperTux Editor
AppVerName=SuperTux Editor 0.3.0
AppPublisher=SuperTux Development Team
AppPublisherURL=http://supertux.berlios.de
AppSupportURL=http://supertux.berlios.de
AppUpdatesURL=http://supertux.berlios.de
DefaultDirName={pf}\SuperTux-Editor
DefaultGroupName=SuperTux Editor
AllowNoIcons=true
VersionInfoVersion=0.3.0
AppVersion=0.3.0
LicenseFile=COPYING.txt
OutputBaseFilename=SuperTux-Editor 0.3.0 Setup
Compression=lzma/ultra
SolidCompression=true
AppID={{5D880A65-B01D-4BE4-AC53-A2D21FE4BEF2}
ShowLanguageDialog=yes
DisableStartupPrompt=true
SetupIconFile=supertux-editor.ico


[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: supertux-editor.exe; DestDir: {app}; Flags: ignoreversion
Source: *.dll; DestDir: {app}; Flags: ignoreversion
Source: data\*.*; DestDir: {app}\data\; Flags: ignoreversion recursesubdirs
Source: supertux-editor.ico; DestDir: {app}; Flags: ignoreversion
Source: COPYING.txt; DestDir: {app}; Flags: ignoreversion
Source: *.pdb; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: {group}\SuperTux Editor; Filename: {app}\supertux-editor.exe
Name: {userdesktop}\SuperTux Editor; Filename: {app}\supertux-editor.exe; Tasks: desktopicon

[Run]
Filename: {app}\supertux-editor.exe; Description: {cm:LaunchProgram,SuperTux Editor}; Flags: nowait postinstall skipifsilent

[Code]
function GetPathInstalled( AppID: String ): String;
var
	sPrevPath: String;
begin
	sPrevPath := '';
	if not RegQueryStringValue(HKLM,
	                           'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1',
	                           'Inno Setup: App Path', sPrevpath) then
		RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1' ,
		                    'Inno Setup: App Path', sPrevpath);

  Result := sPrevPath;
end;

const
	SuperTuxID = '{4BEF4147-E17A-4848-BDC4-60A0AAC70F2A}';

var
	SuperTuxPath: String;

function InitializeSetup(): Boolean;
var
	ErrorCode: Integer;
	NetFrameWorkInstalled: Boolean;
	ResultNET: Boolean;
	ResultSuperTux: Boolean;
begin
	// Check that .NET 2.0 is installed
	NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0');
	if NetFrameWorkInstalled = true then
	begin
		Result := true;
	end;
	if NetFrameWorkInstalled = false then
	begin
		ResultNET := MsgBox('SuperTux Editor requires the .NET 2.0 Framework. Please download and install the .NET 2.0 Framework and run this setup again. Do you want to download the framwork now?',
		                    mbConfirmation, MB_YESNO) = idYes;
		if ResultNET = false then
		begin
			Result := false;
		end
		else
		begin
			Result := false;
			ShellExec('open', 'http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe','','',SW_SHOWNORMAL,ewNoWait,ErrorCode);
		end;
	end;
	//TODO: Check for GTK#

	// Check that SuperTux is installed
	SuperTuxPath := GetPathInstalled(SuperTuxID);
	if (Length(SuperTuxPath) = 0) then
	begin
		ResultSuperTux := MsgBox('SuperTux Editor requires SuperTux 0.3 to be installed. Are you sure you want to continue without installing SuperTux-0.3?',
		                         mbConfirmation, MB_YESNO) = idYes;
		if ResultSuperTux = false then
			Result := false;
	end;

end;
