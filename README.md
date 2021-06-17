# SimpleFlappyBird
Простейший пример игры flappy bird на wpf
По желанию на 100-й строке MainWindow.xaml можно включить плеер, 
```
<audioPlayer:Player Name="CustomPlayer" 
                                Margin="-490,440,0,0"
                                Width="530"
                                Visibility="Visible"/>
```
Фоновую музыку загрузить в 31 строке .cs файла
```
string directory = Environment.CurrentDirectory + "\\..\\..\\";
            Playlist list = new Playlist(new List<Music>
            {
                new Music {source = new Uri(directory + "Путь к треку"), name = "название"},
            });
            CustomPlayer.SetPlayList(list);
 ```
В качестве плеера для фоновой музыки используется [AudioPlayer by StounhandJ](https://github.com/StounhandJ/AudioPlayer) 

В результате работа выглядит следующим образом:

![image](https://user-images.githubusercontent.com/71032698/122448596-96217900-cfad-11eb-9e6a-568bd042d2d5.png)
