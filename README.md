# 🌲♖🌲 The Forest Keep — 2D Action RPG

[![Itch.io](https://img.shields.io/badge/Play_on-Itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white)](https://mirouse.itch.io/the-forest-keep)
[![Unity](https://img.shields.io/badge/Unity-6_(6000.0.6f1)-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-9.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-WebGL_%7C_Windows_%7C_Mobile-7289DA?style=for-the-badge)](https://mirouse.itch.io/the-forest-keep)
[![GitHub Stars](https://img.shields.io/github/stars/Mirouse444/The-Forest-Keep?style=for-the-badge&logo=github)](https://github.com/Mirouse444/The-Forest-Keep/stargazers)

## 🎬 Демонстрация Геймплея
![](Gif/demo1.gif)
![](Gif/demo2.gif)
![](Gif/text.gif)

* **The Forest Keep** - это завершенный пет-проект 2D RPG с полноценным игровым циклом. Проект разработан по современным стандартам геймдева с фокусом на модульную, легко расширяемую архитектуру и оптимизацию.
* 
---

## 🔧 Фреймворки и Библиотеки

* **UniTask:** Асинхронное программирование (async/await), интегрированное в PlayerLoop Unity. Использовано вместо Coroutines для отмены задач (`CancellationToken`) и исключения GC-аллокаций.
* **DOTween:** Процедурная анимация UI и визуальный фидбек (Game Juice) без использования тяжелых аниматоров и `Update`.

---

## 🏛️ Архитектура и Паттерны Проектирования

* **MVP (Model-View-Presenter):** Полная изоляция UI от игровой логики. Позволяет изменять интерфейс без риска сломать механики.
* **Object Pool:** Повторное использование частых объектов (снаряды, VFX, текст урона) для исключения вызовов `Instantiate/Destroy` и устранения просадок FPS из-за сборщика мусора (GC).
* **Data-Driven Design (`ScriptableObjects`):** Вынос баланса, параметров оружия и характеристик в конфигурационные файлы. Позволяет добавлять новый контент и менять баланс без переписывания кода.
* **Finite State Machine (FSM):** Четкое разделение состояний персонажа и ИИ врагов (Idle, Move, Attack, Hit) для легкого расширения поведения и контроля коллизий.

---

## 🚀 Как запустить проект

1. Склонируйте репозиторий.
2. Откройте проект в **Unity 6 (6000.0.6f1)**.
3. Откройте сцену `Assets/Scenes/Menu.unity` и нажмите Play.

---

## 📬 Контакт

* **Email:** [mirouse505@gmail.com]
