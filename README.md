<h1>📋 Task Manager — WPF + MVVM</h1>

<p>
    <img src="https://img.shields.io/badge/C%23-12.0-purple" alt="C#">
    <img src="https://img.shields.io/badge/WPF-.NET%208.0-blue" alt="WPF">
    <img src="https://img.shields.io/badge/Pattern-MVVM-green" alt="MVVM">
</p>

<p>Десктопное приложение для управления задачами, разработанное на <strong>WPF</strong> с использованием паттерна <strong>MVVM</strong>. Приложение позволяет создавать, редактировать, удалять задачи, а также фильтровать и искать их по различным критериям.</p>

<hr>

<h2>✨ Возможности</h2>

<h3>📌 Управление задачами</h3>
<ul>
    <li><strong>➕ Создание</strong> — добавление новой задачи в модальном окне</li>
    <li><strong>✏️ Редактирование</strong> — изменение существующей задачи</li>
    <li><strong>🗑️ Удаление</strong> — с подтверждением действия</li>
    <li><strong>✅ Смена статуса</strong> — отметка выполненных / активных задач</li>
</ul>

<h3>🔍 Фильтрация и поиск</h3>
<ul>
    <li><strong>Фильтр по статусу</strong>: Все / Активные / Завершённые</li>
    <li><strong>🔎 Поиск в реальном времени</strong> — по названию и описанию</li>
    <li><strong>📊 Сортировка</strong> — по дате создания, приоритету или сроку выполнения</li>
</ul>

<h3>🎨 Визуальные возможности</h3>
<ul>
    <li><strong>⚠️ Просроченные задачи</strong> — выделяются розовым фоном</li>
    <li><strong>🎯 Цветовая индикация приоритета</strong>:
        <ul>
            <li>🟢 Low — зелёный</li>
            <li>🟠 Medium — оранжевый</li>
            <li>🔴 High — красный</li>
        </ul>
    </li>
    <li><strong>📋 Завершённые задачи</strong> — полупрозрачные и курсивом</li>
</ul>

<h3>✅ Валидация</h3>
<ul>
    <li>Название задачи обязательно для заполнения</li>
    <li>Дата выполнения не может быть в прошлом (при создании)</li>
</ul>

<h3>💾 Сохранение данных</h3>
<ul>
    <li>Автоматическое сохранение в <strong>JSON-файл</strong></li>
    <li>Загрузка данных при запуске приложения</li>
    <li>Асинхронная работа с файловой системой</li>
</ul>

<hr>

<h2>🛠️ Технологии</h2>

<table>
    <thead>
        <tr>
            <th>Компонент</th>
            <th>Технология</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>UI</td>
            <td>WPF, XAML</td>
        </tr>
        <tr>
            <td>Архитектура</td>
            <td>MVVM, C#</td>
        </tr>
        <tr>
            <td>Хранение данных</td>
            <td>JSON (<code>System.Text.Json</code>)</td>
        </tr>
        <tr>
            <td>Коллекции</td>
            <td><code>ObservableCollection&lt;T&gt;</code>, <code>ICollectionView</code></td>
        </tr>
        <tr>
            <td>Команды</td>
            <td><code>RelayCommand</code> с <code>CanExecute</code></td>
        </tr>
        <tr>
            <td>Конвертеры</td>
            <td><code>IValueConverter</code> (приоритет → цвет, дата → стиль)</td>
        </tr>
    </tbody>
</table>

<hr>

<h2>🚀 Запуск проекта</h2>

<h3>📋 Системные требования</h3>
<ul>
    <li><strong>Операционная система</strong>: Windows 10 / 11</li>
    <li><strong>SDK</strong>: <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">.NET 8.0 SDK</a> или новее</li>
    <li><strong>IDE</strong>: Visual Studio 2022 (рекомендуется) или любой другой редактор с поддержкой C#</li>
</ul>

<h3>📦 Клонирование репозитория</h3>

<pre><code>git clone https://github.com/BakaLaver/TaskManager.git
cd TaskManager</code></pre>

<h3>🏗️ Сборка и запуск</h3>

<h4>Вариант 1: Через Visual Studio (рекомендуемый)</h4>
<ol>
    <li>Откройте решение <code>EnSoftGroopTest.sln</code></li>
    <li>Выберите конфигурацию <strong>Debug</strong> или <strong>Release</strong></li>
    <li>Нажмите <code>F5</code> или кнопку <strong>Start</strong> для запуска</li>
</ol>

<h4>Вариант 2: Через командную строку</h4>

<pre><code># Перейти в папку проекта
cd EnSoftGroopTest

# Восстановить зависимости
dotnet restore

# Собрать проект
dotnet build --configuration Release

# Запустить приложение
dotnet run --configuration Release</code></pre>

<h4>Вариант 3: Запуск скомпилированного приложения</h4>

<pre><code># Перейти в папку со сборкой
cd EnSoftGroopTest\bin\Release\net8.0-windows\

# Запустить .exe файл
EnSoftGroopTest.exe</code></pre>

<hr>

<h2>📁 Структура проекта</h2>

<pre><code>EnSoftGroopTest/
├── Commands/
│   └── RelayCommand.cs                 # Базовая реализация ICommand
├── Converters/
│   ├── PriorityToColorConverter.cs      # Приоритет → цвет
│   ├── DueDateToBrushConverter.cs       # Дата → цвет фона (просрочено)
│   ├── StatusToBooleanConverter.cs      # Статус → bool для CheckBox
│   └── BooleanToInverseBooleanConverter.cs # Инверсия bool для IsEnabled
├── Models/
│   ├── Enums.cs                          # TaskStatus, TaskPriority
│   └── TaskItem.cs                       # Модель задачи с INotifyPropertyChanged
├── Services/
│   ├── IFileService.cs                    # Интерфейс сервиса файлов
│   └── FileService.cs                      # Работа с JSON (async)
├── ViewModels/
│   ├── MainViewModel.cs                    # Главная VM (список задач)
│   └── TaskEditViewModel.cs                 # VM для окна редактирования
├── Views/
│   ├── MainWindow.xaml                      # Главное окно
│   └── TaskEditWindow.xaml                   # Окно редактирования задачи
└── App.xaml                                   # Точка входа</code></pre>

<hr>

<h2>💾 Файл с данными</h2>

<p>При первом запуске файл <code>tasks.json</code> <strong>создаётся автоматически</strong> при сохранении первой задачи.</p>

<p><strong>Расположение файла:</strong></p>
<ul>
    <li>При запуске из Visual Studio:<br>
        <code>[Папка проекта]\EnSoftGroopTest\bin\Debug\net8.0-windows\tasks.json</code></li>
    <li>При запуске собранного приложения:<br>
        <code>[Папка с программой]\tasks.json</code></li>
</ul>

<p><strong>Пример содержимого:</strong></p>

<pre><code>[
  {
    "Id": "550e8400-e29b-41d4-a716-446655440000",
    "Title": "Купить продукты",
    "Description": "Молоко, хлеб, яйца",
    "Status": "Active",
    "Priority": "Medium",
    "DueDate": "2026-03-20T00:00:00",
    "CreatedAt": "2026-03-14T10:30:00"
  }
]</code></pre>

<hr>

<h2>🧪 Тестирование функционала</h2>

<h3>Проверка всех возможностей:</h3>

<ol>
    <li>
        <strong>Создание задачи</strong>:
        <ul>
            <li>Нажмите "Добавить"</li>
            <li>Заполните форму (название обязательно)</li>
            <li>Нажмите "Сохранить"</li>
        </ul>
    </li>
    <li>
        <strong>Редактирование задачи</strong>:
        <ul>
            <li>Выберите задачу в списке</li>
            <li>Нажмите "Редактировать"</li>
            <li>Измените данные, включая статус (доступен только для существующих задач)</li>
        </ul>
    </li>
    <li>
        <strong>Изменение статуса</strong>:
        <ul>
            <li>Кликните на чекбокс в списке задач</li>
            <li>Или через контекстное меню (правый клик → "Выполнено / Активно")</li>
        </ul>
    </li>
    <li>
        <strong>Фильтрация</strong>:
        <ul>
            <li>Выберите фильтр: Все / Активные / Завершённые</li>
        </ul>
    </li>
    <li>
        <strong>Поиск</strong>:
        <ul>
            <li>Введите текст в поле поиска — результаты обновляются в реальном времени</li>
        </ul>
    </li>
    <li>
        <strong>Сортировка</strong>:
        <ul>
            <li>Кликните на заголовок колонки для сортировки</li>
        </ul>
    </li>
    <li>
        <strong>Удаление</strong>:
        <ul>
            <li>Выберите задачу → "Удалить" (или контекстное меню)</li>
            <li>Подтвердите действие</li>
        </ul>
    </li>
</ol>

<hr>

<h2>⚠️ Возможные проблемы и решения</h2>

<table>
    <thead>
        <tr>
            <th>Проблема</th>
            <th>Решение</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Не сохраняются задачи</td>
            <td>Проверьте права на запись в папку с приложением. Файл <code>tasks.json</code> должен создаваться в папке с <code>.exe</code> файлом.</td>
        </tr>
        <tr>
            <td>Не работают конвертеры</td>
            <td>Пересоберите проект (Rebuild Solution). Убедитесь, что все конвертеры находятся в папке <code>Converters</code> и имеют правильное пространство имён.</td>
        </tr>
        <tr>
            <td>Ошибки привязки в логах</td>
            <td>Проверьте <code>RelativeSource</code> в привязках команд контекстного меню — должен указывать на <code>ListView</code>, а не на <code>Window</code>.</td>
        </tr>
    </tbody>
</table>

<hr>
