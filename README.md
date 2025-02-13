# Skyward Explorer :video_game:

2D-платформер с динамичной боевой системой, возможностью перекатов, блоков и улучшения характеристик персонажа. 

[Скачать билд](https://www.dropbox.com/scl/fi/syy6h8rp3k8epkrrdjayi/Build.rar?rlkey=8zk1hfy10aj5hqea9a20895qe&st=shqovq82&dl=0)
## :movie_camera: Геймплей 

### :pushpin: Основные механики:
:heavy_check_mark: Перекаты с временной неуязвимостью  
:heavy_check_mark: Блок с задержкой  
:heavy_check_mark: Ai для мобов  
![Если вовремя попасть в тайминг можно нанести блоком урон врагу](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/animation.gif?raw=true)  
:heavy_check_mark: Прокачка статов персонажа  
![Прокачка статов](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/Stats.gif?raw=true)
:heavy_check_mark: Инвентарь с динамическим UI  
![Инвентарь](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/inventory.gif?raw=true) 
:heavy_check_mark: Мини-игра по мотивам Among Us  
![Мини-игра](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/MiniGame.gif?raw=true) 

## :hammer: Техническая реализация

### :pushpin: Использованные технологии:
- Unity(2022.3.16f1)
- C#
- Cinemachine для камеры
- ScriptableObjects для хранения данных

## :large_blue_diamond: Структура кода

### :hammer: Основные скрипты:
---
**Creature.cs**  — базовый скрипт для всех существ в игре

#### :small_blue_diamond: Основные функции:
:heavy_check_mark: **Передвижение** – обработка движения по оси X и Y.  
:heavy_check_mark: **Анимация** – управление анимациями в зависимости от состояния.  
:heavy_check_mark: **Обнаружение препятствий** – определение столкновений с землёй и объектами.  
:heavy_check_mark: **Физика** – работа с Rigidbody2D для корректного передвижения.  

---
**HeroInputReader.cs** —  отвечает за обработку пользовательского ввода.

#### :small_blue_diamond: Основные функции:
:heavy_check_mark: **Движение**  
:heavy_check_mark: **Атака**    
:heavy_check_mark: **Перекат**  
:heavy_check_mark: **Блок**  
:heavy_check_mark: **Взаимодействие**  
:heavy_check_mark: **Лечение**  
 
---
**GameSession.cs** — управление игровой сессией 
Этот скрипт отвечает за сохранение и управление прогрессом игрока во время игры и между уровнями.

#### :small_blue_diamond: Основные функции:
:heavy_check_mark: **Singleton** – гарантирует, что в игре всегда существует только один экземпляр GameSession.  
:heavy_check_mark: **Хранение данных** – содержит информацию о состоянии персонажа (здоровье, ресурсы и т. д.).  
:heavy_check_mark: **Перенос данных между сценами** – сохраняет прогресс и передаёт его между уровнями.  
:heavy_check_mark: **Обновление состояния** – обновляет данные игры при изменениях.

---
**HudController.cs** —  отвечает за интерфейс.

#### :small_blue_diamond: Основные функции:
:heavy_check_mark: **Отображение здоровья игрока**  
:heavy_check_mark: **Реакция на изменения здоровья** — если персонаж получает урон или лечится, HUD обновляется.  
:heavy_check_mark: **Кнопка прокачки статов** — становится активной, если игрок разблокировал улучшение.  
:heavy_check_mark: **Открытие меню**

---
**Система репозитория данных в игре** — отвечает за хранение и доступ к данным (предметы, умения, характеристики игрока). Она построена на ScriptableObject и использует общий базовый класс DefRepository<T>.

#### :small_blue_diamond: Основные возможности системы
:heavy_check_mark: **Единый источник данных** — все игровые данные хранятся в ScriptableObject, который загружается из Resources.  
:heavy_check_mark: **Гибкость** — можно легко добавлять новые типы данных (предметы, способности, характеристики).  
:heavy_check_mark: **Централизованный доступ** — все данные доступны через DefsFacade, что позволяет избежать дублирования кода.  
:heavy_check_mark: **Поиск по ID** — быстрый доступ к нужному объекту по ID.

---

## :art: Графика и анимации

### :pushpin: Что использовано:
Так как проект является учебным, использовались бесплатные ассеты с AssetStore. Ссылки:  
[Главный герой](https://assetstore.unity.com/packages/2d/characters/hero-knight-pixel-art-165188)  
[Враг - бандит](https://assetstore.unity.com/packages/2d/characters/bandits-pixel-art-104130)  
[Кот дух](https://assetstore.unity.com/packages/2d/characters/bandits-pixel-art-104130)  
[Босс и летающий глаз](https://assetstore.unity.com/packages/2d/characters/monsters-creatures-fantasy-167949)  
[Окружение](https://assetstore.unity.com/packages/2d/environments/platformer-fantasy-set1-159063)  
[Сердца, зелья, двери](https://assetstore.unity.com/packages/2d/environments/classic-legacy-pack-village-233288)

Некоторые из этих ассетов были немного перерисованы и\или перекрашены под мои нужды в Aseprite.  
![Пример изменения спрайта](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/HeroKnight.png?raw=true)

## :ghost: AI и логика врагов

### :pushpin: Основные особенности:
:heavy_check_mark: Следование за игроком  
:heavy_check_mark: Разные уровни агрессии  

![механики босса](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/bossSceleton.gif?raw=true)

## :bar_chart: Оптимизация и производительность

### :pushpin: Меры оптимизации:
- Object Pool  
- Использование Sprite Atlas для графики  
- Profiler и Memory Analyzer для поиска утечек памяти  
- Минимизация использования Update()

![Profiler](https://github.com/osk8bit/Skyward-Explorer/blob/master/images/Profiler.png?raw=true)

## :trophy: Чему я научилась в этом проекте

:heavy_check_mark: Опыт работы с Cinemachine  
:heavy_check_mark: Навык оптимизации в Unity  
:heavy_check_mark: Использование событий и делегатов  
:heavy_check_mark: Знакомство с паттернами (Singleton, Event-Driven, MVC)

## :computer: Как запустить проект

Первый способ:
- Открыть в Unity
- Запустить сцену MainMenu.unity
- Наслаждаться игрой

Второй способ:  
[Скачать билд](https://www.dropbox.com/scl/fi/syy6h8rp3k8epkrrdjayi/Build.rar?rlkey=8zk1hfy10aj5hqea9a20895qe&st=shqovq82&dl=0)
