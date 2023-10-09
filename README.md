# RestTest
Основа приложения:
1) Di framework - VContainer - https://vcontainer.hadashikick.jp
2) UniRx - https://github.com/neuecc/UniRx

UI:
Для работы с Ui используется кастомный WindowManager.

StateMachine:
В качестве некой StateMachine использую простую стейт машину которая вкупе с UniRx позволяет базого регулировать переключение сцен и игровые состояния - https://github.com/erproxy/RestTest/blob/main/RestTest/Assets/Scripts/RestTest/StateMachine/CoreStateMachine.cs

Запросы:
Для работы с запросами был написан небольшой сервис для загрузки json и для загрузки картинки - https://github.com/erproxy/RestTest/blob/main/RestTest/Assets/Scripts/Models/WebTool/WebToolService.cs

Сохранение локальное:
Сохранение данных реализовал через json и сервис который вкупе с UniRx предоставляет работу с данными - 
https://github.com/erproxy/RestTest/tree/main/RestTest/Assets/Scripts/Models/DataModels
https://github.com/erproxy/RestTest/blob/main/RestTest/Assets/Scripts/Tools/GameTools/JsonSerialization.cs
Все модельки данных имеют вид структур.
