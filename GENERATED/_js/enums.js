﻿
 var enum_typeTRF = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Общий тариф',-1]
,['Тариф подрядчика',0]
,['Тариф клиента',1]
 ]});
 var enum_StructType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Строка атрибутов',0]
,['Коллекция',1]
,['Дерево',2]
 ]});
 var enum_SaleRent = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Продажа',-1]
,['Аренда',0]
 ]});
 var enum_WFFuncParam = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Значение',0]
,['Значение из параметра',1]
,['Выражение',2]
,['Папка',3]
,['Документ процесса',4]
,['Документ',5]
,['Раздел',6]
,['Поле',7]
,['Роль',8]
,['Тип документа',9]
 ]});
 var enum_ReportType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Таблица',0]
,['Двумерная матрица',1]
,['Только расчет',2]
,['Экспорт по WORD шаблону',3]
,['Экспорт по Excel шаблону',4]
 ]});
 var enum_Education = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Не важно',-1]
,['Неполное среднее',0]
,['Среднее',1]
,['Среднее специальное',2]
,['Неполное высшее',3]
,['Высшее',4]
,['Несколько высших',5]
 ]});
 var enum_KONTR_STATUS = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Активеный',0]
,['Пссивный',1]
,['Блокирован',2]
,['Операции запрещены',3]
 ]});
 var enum_TypeStyle = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Скалярный тип',0]
,['Выражение',1]
,['Перечисление',2]
,['Интервал',3]
,['Ссылка',4]
,['Элемент оформления',5]
 ]});
 var enum_ReplicationType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Весь документ',0]
,['Построчно',1]
,['Локальный',2]
 ]});
 var enum_DCType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Остатки',-1]
,['Дебит',0]
,['Кредит',1]
 ]});
 var enum_NumerationRule = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Единая зона',0]
,['По году',1]
,['По кварталу',2]
,['По месяцу',3]
,['По дню',4]
,['Произвольные зоны',10]
 ]});
 var enum_WFProcessState = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Initial',0]
,['Prepare',1]
,['Active',2]
,['Pause',3]
,['Done',4]
,['Processed',5]
 ]});
 var enum_MenuActionType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Ничего не делать',0]
,['Открыть документ',1]
,['Выполнить метод',2]
,['Открыть журнал',3]
,['Запустить АРМ',4]
,['Открыть отчет',5]
 ]});
 var enum_typeOperatorSystem = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Прочее',0]
,['Оператор',1]
,['Менеджер',2]
 ]});
 var enum_typeOrder = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Безадресный заказ',0]
,['Адресный заказ',1]
 ]});
 var enum_WFShortcutType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Document',0]
,['Function',1]
,['Process',2]
 ]});
 var enum_VHAlignment = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Left Top',0]
,['Left Center',1]
,['Left Bottom',2]
,['Center Top',3]
,['Center Center',4]
,['Center Bottom',5]
,['Right Top',6]
,['Right Center',7]
,['Right Bottom',8]
 ]});
 var enum_typeDocOrder = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Наличный (товарный чек + кас.чек)',0]
,['Наличный (ПКО + кас.чек)',1]
,['Безналичный расчет',3]
 ]});
 var enum_TypeSempling = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Клиенты',-1]
,['Адресаты операторов',0]
,['Список адресатов',1]
,['Список адресов СПб',2]
 ]});
 var enum_CurrencyType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Рубль',0]
,['Доллар',1]
,['Евро',2]
 ]});
 var enum_InfoStoreType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
[' Общий',0]
,['Персональный',1]
,['Групповой',2]
 ]});
 var enum_B2Market = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Квартира в новостройках',1]
,['Объект вторичного рынка',2]
,['Загородный объект',3]
,['Коммерческие объект',4]
,['Зарубежный объект',5]
,['Жилая аренда',6]
 ]});
 var enum_DevelopmentBase = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['VB6',0]
,['DOTNET',1]
,['JAVA',2]
,['OTHER',3]
 ]});
 var enum_Quarter = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['?',0]
,['I',1]
,['II',2]
,['III',3]
,['IV',4]
 ]});
 var enum_Months = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Январь',1]
,['Февраль',2]
,['Март',3]
,['Апрель',4]
,['Май',5]
,['Июнь',6]
,['Июль',7]
,['Август',8]
,['Сентябрь',9]
,['Октябрь',10]
,['Ноябрь',11]
,['Декабрь',12]
 ]});
 var enum_typePackage = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Все',-1]
,['Документы',0]
,['Товар',1]
 ]});
 var enum_ColumnSortType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['As String',0]
,['As Numeric',1]
,['As Date',2]
 ]});
 var enum_HACCPPStep = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Начало процесса',0]
,['Завершение процесса',1]
,['Останов процесса',2]
,['Операция',3]
,['Решение',4]
,['Сырье',5]
,['Хранение',6]
,['Транспортировка',7]
,['Выход в',8]
,['Вход из',9]
 ]});
 var enum_Boolean = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Да',-1]
,['Нет',0]
 ]});
 var enum_VRTaskType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['В определенный момент',0]
,['К определенному времени',1]
,['Фоновая',2]
 ]});
 var enum_JournalLinkType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Нет',0]
,['Ссылка на объект',1]
,['Ссылка на строку',2]
,['Связка InstanceID (в передлах объекта)',3]
,['Связка ParentStructRowID  (в передлах объекта)',4]
 ]});
 var enum_TargetType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['СУБД',0]
,['МОДЕЛЬ',1]
,['Приложение',2]
,['Документация',3]
,['АРМ',4]
 ]});
 var enum_AdditionType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Вес',0]
,['Объем',1]
,['Плотность',2]
,['Прочее',3]
 ]});
 var enum_PCB_LayerP = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Не имеет значения',0]
,['Позитив',1]
,['Негатив',2]
 ]});
 var enum_typeClient = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Разовый',0]
 ]});
 var enum_CacheType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Только свои',0]
,['Подчиненные',1]
,['Все',2]
 ]});
 var enum_typeRowCheque = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Прочее',0]
,['ПО',1]
,['доп.услуга ПО',2]
,['доп.услуга заказа',3]
,['Простое ПО',4]
,['Товар',5]
 ]});
 var enum_MesureFormat = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Число',0]
,['Дата',1]
,['Справочник',2]
,['Объект',4]
,['Текст',5]
 ]});
 var enum_ExportType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Нет',0]
,['Сайт',1]
,['Сайт и МБ',3]
 ]});
 var enum_WFStepClass = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['SimpleFunction',0]
,['StartFunction',1]
,['StopFunction',2]
,['PeriodicFunction',3]
 ]});
 var enum_HACCPStep = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Начало процесса',0]
,['Завершение процесса',1]
,['Останов процесса',2]
,['Операция',3]
,['Контрольная точка',4]
,['Корректирующее действие',5]
,['Процесс',6]
 ]});
 var enum_typePersonPay = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Отправитель',0]
,['Получатель',1]
 ]});
 var enum_DayInWeek = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Понедельник',1]
,['Вторник',2]
,['Среда',3]
,['Четверг',4]
,['Пятница',5]
,['Суббота',6]
,['Воскресенье',7]
 ]});
 var enum_GeneratorStyle = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Один тип',0]
,['Все типы сразу',1]
 ]});
 var enum_PlatType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Отправитель',0]
,['Получатель',1]
,['Другой',2]
 ]});
 var enum_msgState = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Состояние заявки',0]
,['Сообщено абоненту',1]
,['Абонент не ответил',2]
,['Промежуточный ответ',3]
 ]});
 var enum_KONTR_TYPE = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Покупатель',0]
,['Поставщик',1]
,['Партнер',2]
,['Учредитель',3]
,['Перевозчик',4]
,['Прочее',5]
 ]});
 var enum_typeNomen = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Документ',0]
,['Товар',1]
,['Листовки',2]
,['Письмо',3]
,['Газеты, журналы, брошюры',4]
 ]});
 var enum_OnJournalRowClick = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Ничего не делать',0]
,['Открыть строку',1]
,['Открыть документ',2]
 ]});
 var enum_PartType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Строка',0]
,['Коллекция',1]
,['Дерево',2]
,['Расширение',3]
,['Расширение с данными',4]
 ]});
 var enum_typePay = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Сдельная',0]
,['Ставка',1]
 ]});
 var enum_ReferenceType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Скалярное поле ( не ссылка)',0]
,['На объект ',1]
,['На строку раздела',2]
,['На источник данных',3]
 ]});
 var enum_stateNomen = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Оформляется',0]
,['Принято',2]
,['В процессе',3]
,['Доставлено',4]
,['Возврат',5]
,['Переадресация',6]
 ]});
 var enum_ConditionType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['none',0]
,['=',1]
,['<>',2]
,['>',3]
,['>=',4]
,['<',6]
,['<=',7]
,['like',8]
 ]});
 var enum_FolderType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['cls__',0]
,['Входящие',1]
,['Исходящие',2]
,['Удаленные',3]
,['Журнал',4]
,['Календарь',5]
,['Отправленные',6]
,['Черновики',7]
,['В работе',8]
,['Отложенные',9]
,['Завершенные',10]
 ]});
 var enum_msgResult = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Результат',0]
,['В работе',1]
,['Выполнено',2]
 ]});
 var enum_HaccpIdent = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Внешний вид',0]
,['Цвет',1]
,['Запах',2]
,['Форма',3]
,['Вкус',4]
,['Звук',5]
,['Качество поверхности',6]
,['Прочее',100]
 ]});
 var enum_PartAddBehaivor = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['AddForm',0]
,['RefreshOnly',1]
,['RunAction',2]
 ]});
 var enum_ExtentionType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['StatusExt',0]
,['OnFormExt',1]
,['CustomExt',2]
,['JrnlAddExt',3]
,['JrnlRunExt',4]
,['DefaultExt',5]
,['VerifyRowExt',6]
,['CodeGenerator',7]
,['ARMGenerator',8]
 ]});
 var enum_typeCourier = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Договор подряда',0]
,['Трудовая книжка',1]
 ]});
 var enum_typeFace = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['ФизЛицо',0]
,['ЮрЛицо',1]
 ]});
 var enum_ContactStyle = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Прочее',0]
,['Телефон',1]
,['E-Mail',2]
 ]});
 var enum_Sex = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Женский',-1]
,['Не существенно',0]
,['Мужской',1]
 ]});
 var enum_YesNo = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Нет',0]
,['Да',1]
 ]});
 var enum_AggregationType = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['none',0]
,['AVG',1]
,['COUNT',2]
,['SUM',3]
,['MIN',4]
,['MAX',5]
,['CUSTOM',6]
 ]});
 var enum_WFFuncState = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Initial',0]
,['Prepare',1]
,['Active',2]
,['InWork',3]
,['Pause',4]
,['Ready',5]
,['InControl',6]
,['Done',7]
,['Processed',8]
 ]});
 var enum_Employment = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Не важно',-1]
,['Полная',0]
,['Частичная',1]
 ]});
 var enum_DeliveryOn = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Международная',0]
,['Страна',1]
,['Страна2',2]
,['Субъект',3]
,['Город',4]
 ]});
 var enum_typeReceiv = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['СВС',0]
,['ПТС',1]
 ]});
 var enum_TriState = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Не существенно',-1]
,['Нет',0]
,['Да',1]
 ]});
 var enum_typeTariff = Ext.create('Ext.data.ArrayStore', {
  fields: [ {name: 'name'}, {name: 'value',     type: 'int'} ], data: [ 
['Доставка',0]
,['Услуга',1]
 ]});