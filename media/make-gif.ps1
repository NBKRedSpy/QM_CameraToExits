# Origional version from https://superuser.com/questions/556029/how-do-i-convert-a-video-to-gif-using-ffmpeg-with-reasonable-quality#556031 
#
# ffmpeg -ss 30 -t 3 -i input.mp4 \
#     -vf "fps=10,scale=320:-1:flags=lanczos,split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse" \
#     -loop 0 output.gif


# x:\ffmpeg.exe -i .\CameraMove.mp4 -vf "fps=18,scale=480:-1:flags=lanczos,split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse" -loop 0 .\CameraMove.gif
x:\ffmpeg.exe -i .\CameraMove.mp4 -y -vf "fps=30,scale=390:-1:flags=lanczos,split[s0][s1];[s0]palettegen[p];[s1][p]paletteuse" -loop 0 .\CameraMove.gif