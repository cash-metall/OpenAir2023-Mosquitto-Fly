@echo off
setlocal EnableDelayedExpansion
set APP="bin\Editor_double_x64.exe"
if exist %APP% (
	start "" %APP% -video_app vk -video_refresh 0 -video_debug 0 -main_window_size 2560 1440 -main_window_resizable 1 -main_window_fullscreen 0 -render_vsync 0 -video_offscreen 0 -sound_app auto -data_path ../data/ -extern_plugin UnigineFbxImporter,UnigineGLTFImporter,UnigineFbxExporter -console_command "config_autosave 1 && world_load \"mosquito\""
) else (
	set MESSAGE=%APP% not found"
	echo !MESSAGE!
)