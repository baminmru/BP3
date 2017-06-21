

Option Explicit On

Imports System.xml
Imports BP3
Imports BP3.Document

Namespace bp3card




''' <summary>
'''Перечисление Тип тарифа СС
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeTRF'Тип тарифа СС
  typeTRF_Tarif_podrydcika=0' Тариф подрядчика
  typeTRF_Tarif_klienta=1' Тариф клиента
  typeTRF_Obsiy_tarif=-1' Общий тариф
end enum 


''' <summary>
'''Перечисление Тип раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumStructType'Тип раздела
  StructType_Stroka_atributov=0' Строка атрибутов
  StructType_Kollekciy=1' Коллекция
  StructType_Derevo=2' Дерево
end enum 


''' <summary>
'''Перечисление Арендапродажа
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumSaleRent'Арендапродажа
  SaleRent_Prodaga=-1' Продажа
  SaleRent_Arenda=0' Аренда
end enum 


''' <summary>
'''Перечисление Вариант расшифровки параметра функции
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumWFFuncParam'Вариант расшифровки параметра функции
  WFFuncParam_Rol_=8' Роль
  WFFuncParam_Viragenie=2' Выражение
  WFFuncParam_Dokument=5' Документ
  WFFuncParam_Pole=7' Поле
  WFFuncParam_Tip_dokumenta=9' Тип документа
  WFFuncParam_Znacenie=0' Значение
  WFFuncParam_Razdel=6' Раздел
  WFFuncParam_Dokument_processa=4' Документ процесса
  WFFuncParam_Papka=3' Папка
  WFFuncParam_Znacenie_iz_parametra=1' Значение из параметра
end enum 


''' <summary>
'''Перечисление Вариант отчета
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumReportType'Вариант отчета
  ReportType_Eksport_po_Excel_sablonu=4' Экспорт по Excel шаблону
  ReportType_Tablica=0' Таблица
  ReportType_Eksport_po_WORD_sablonu=3' Экспорт по WORD шаблону
  ReportType_Dvumernay_matrica=1' Двумерная матрица
  ReportType_Tol_ko_rascet=2' Только расчет
end enum 


''' <summary>
'''Перечисление Образование
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumEducation'Образование
  Education_Ne_vagno=-1' Не важно
  Education_Srednee=1' Среднее
  Education_Vissee=4' Высшее
  Education_Nepolnoe_vissee=3' Неполное высшее
  Education_Nepolnoe_srednee=0' Неполное среднее
  Education_Neskol_ko_vissih=5' Несколько высших
  Education_Srednee_special_noe=2' Среднее специальное
end enum 


''' <summary>
'''Перечисление Состояние партнера
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumKONTR_STATUS'Состояние партнера
  KONTR_STATUS_Blokirovan=2' Блокирован
  KONTR_STATUS_Pssivniy=1' Пссивный
  KONTR_STATUS_Operacii_zapreseni=3' Операции запрещены
  KONTR_STATUS_Aktiveniy=0' Активеный
end enum 


''' <summary>
'''Перечисление Вариант трактовки типа поля
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumTypeStyle'Вариант трактовки типа поля
  TypeStyle_Ssilka=4' Ссылка
  TypeStyle_Viragenie=1' Выражение
  TypeStyle_Element_oformleniy=5' Элемент оформления
  TypeStyle_Interval=3' Интервал
  TypeStyle_Perecislenie=2' Перечисление
  TypeStyle_Skalyrniy_tip=0' Скалярный тип
end enum 


''' <summary>
'''Перечисление Вариант репликации докуента
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumReplicationType'Вариант репликации докуента
  ReplicationType_Postrocno=1' Построчно
  ReplicationType_Ves__dokument=0' Весь документ
  ReplicationType_Lokal_niy=2' Локальный
end enum 


''' <summary>
'''Перечисление Остатки/Дебит/Кредит
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumDCType'Остатки/Дебит/Кредит
  DCType_Ostatki=-1' Остатки
  DCType_Kredit=1' Кредит
  DCType_Debit=0' Дебит
end enum 


''' <summary>
'''Перечисление Правило нумерации
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumNumerationRule'Правило нумерации
  NumerationRule_Po_kvartalu=2' По кварталу
  NumerationRule_Po_mesycu=3' По месяцу
  NumerationRule_Edinay_zona=0' Единая зона
  NumerationRule_Po_dnu=4' По дню
  NumerationRule_Po_godu=1' По году
  NumerationRule_Proizvol_nie_zoni=10' Произвольные зоны
end enum 


''' <summary>
'''Перечисление Состояния процесса
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumWFProcessState'Состояния процесса
  WFProcessState_Pause=3' Pause
  WFProcessState_Active=2' Active
  WFProcessState_Done=4' Done
  WFProcessState_Prepare=1' Prepare
  WFProcessState_Initial=0' Initial
  WFProcessState_Processed=5' Processed
end enum 


''' <summary>
'''Перечисление Вариант действия при выборе пункта меню
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumMenuActionType'Вариант действия при выборе пункта меню
  MenuActionType_Zapustit__ARM=4' Запустить АРМ
  MenuActionType_Vipolnit__metod=2' Выполнить метод
  MenuActionType_Otkrit__otcet=5' Открыть отчет
  MenuActionType_Nicego_ne_delat_=0' Ничего не делать
  MenuActionType_Otkrit__dokument=1' Открыть документ
  MenuActionType_Otkrit__gurnal=3' Открыть журнал
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeOperatorSystem'
  typeOperatorSystem_Menedger=2' Менеджер
  typeOperatorSystem_Operator=1' Оператор
  typeOperatorSystem_Procee=0' Прочее
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeOrder'
  typeOrder_Bezadresniy_zakaz=0' Безадресный заказ
  typeOrder_Adresniy_zakaz=1' Адресный заказ
end enum 


''' <summary>
'''Перечисление Варианты ярлыков, которые может размещать процесс
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumWFShortcutType'Варианты ярлыков, которые может размещать процесс
  WFShortcutType_Document=0' Document
  WFShortcutType_Process=2' Process
  WFShortcutType_Function=1' Function
end enum 


''' <summary>
'''Перечисление Выравнивание
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumVHAlignment'Выравнивание
  VHAlignment_Right_Top=6' Right Top
  VHAlignment_Right_Center=7' Right Center
  VHAlignment_Right_Bottom=8' Right Bottom
  VHAlignment_Center_Top=3' Center Top
  VHAlignment_Left_Top=0' Left Top
  VHAlignment_Center_Center=4' Center Center
  VHAlignment_Left_Center=1' Left Center
  VHAlignment_Center_Bottom=5' Center Bottom
  VHAlignment_Left_Bottom=2' Left Bottom
end enum 


''' <summary>
'''Перечисление Тип документов для печати заказчику
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeDocOrder'Тип документов для печати заказчику
  typeDocOrder_Beznalicniy_rascet=3' Безналичный расчет
  typeDocOrder_Nalicniy_OPNPKO_PLS_kasDOTcekCLS=1' Наличный (ПКО + кас.чек)
  typeDocOrder_Nalicniy_OPNtovarniy_cek_PLS_kasDOTcekCLS=0' Наличный (товарный чек + кас.чек)
end enum 


''' <summary>
'''Перечисление Тип выборки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumTypeSempling'Тип выборки
  TypeSempling_Spisok_adresov_SPb=2' Список адресов СПб
  TypeSempling_Adresati_operatorov=0' Адресаты операторов
  TypeSempling_Klienti=-1' Клиенты
  TypeSempling_Spisok_adresatov=1' Список адресатов
end enum 


''' <summary>
'''Перечисление Валюта платежа
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumCurrencyType'Валюта платежа
  CurrencyType_Evro=2' Евро
  CurrencyType_Rubl_=0' Рубль
  CurrencyType_Dollar=1' Доллар
end enum 


''' <summary>
'''Перечисление Тип каталога
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumInfoStoreType'Тип каталога
  InfoStoreType_Gruppovoy=2' Групповой
  InfoStoreType_cls__Obsiy=0'  Общий
  InfoStoreType_Personal_niy=1' Персональный
end enum 


''' <summary>
'''Перечисление Рынок
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumB2Market'Рынок
  B2Market_Kommerceskie_ob_ekt=4' Коммерческие объект
  B2Market_Ob_ekt_vtoricnogo_rinka=2' Объект вторичного рынка
  B2Market_Gilay_arenda=6' Жилая аренда
  B2Market_Zagorodniy_ob_ekt=3' Загородный объект
  B2Market_Zarubegniy_ob_ekt=5' Зарубежный объект
  B2Market_Kvartira_v_novostroykah=1' Квартира в новостройках
end enum 


''' <summary>
'''Перечисление Платформа разработки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumDevelopmentBase'Платформа разработки
  DevelopmentBase_OTHER=3' OTHER
  DevelopmentBase_DOTNET=1' DOTNET
  DevelopmentBase_JAVA=2' JAVA
  DevelopmentBase_VB6=0' VB6
end enum 


''' <summary>
'''Перечисление Квартал
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumQuarter'Квартал
  Quarter_I=1' I
  Quarter_IV=4' IV
  Quarter_QMARK=0' ?
  Quarter_II=2' II
  Quarter_III=3' III
end enum 


''' <summary>
'''Перечисление Месяцы
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumMonths'Месяцы
  Months_May=5' Май
  Months_Sentybr_=9' Сентябрь
  Months_Iun_=6' Июнь
  Months_Dekabr_=12' Декабрь
  Months_Ynvar_=1' Январь
  Months_Avgust=8' Август
  Months_Fevral_=2' Февраль
  Months_Aprel_=4' Апрель
  Months_Iul_=7' Июль
  Months_Oktybr_=10' Октябрь
  Months_Mart=3' Март
  Months_Noybr_=11' Ноябрь
end enum 


''' <summary>
'''Перечисление Тип отправления
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypePackage'Тип отправления
  typePackage_Dokumenti=0' Документы
  typePackage_Vse=-1' Все
  typePackage_Tovar=1' Товар
end enum 


''' <summary>
'''Перечисление Вариант сортиовки данных колонки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumColumnSortType'Вариант сортиовки данных колонки
  ColumnSortType_As_String=0' As String
  ColumnSortType_As_Numeric=1' As Numeric
  ColumnSortType_As_Date=2' As Date
end enum 


''' <summary>
'''Перечисление Тип шага в ХАССП процессе
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumHACCPPStep'Тип шага в ХАССП процессе
  HACCPPStep_Vhod_iz=9' Вход из
  HACCPPStep_Operaciy=3' Операция
  HACCPPStep_Ostanov_processa=2' Останов процесса
  HACCPPStep_Hranenie=6' Хранение
  HACCPPStep_Transportirovka=7' Транспортировка
  HACCPPStep_Vihod_v=8' Выход в
  HACCPPStep_Sir_e=5' Сырье
  HACCPPStep_Zaversenie_processa=1' Завершение процесса
  HACCPPStep_Nacalo_processa=0' Начало процесса
  HACCPPStep_Resenie=4' Решение
end enum 


''' <summary>
'''Перечисление Да / Нет
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumBoolean'Да / Нет
  Boolean_Da=-1' Да
  Boolean_Net=0' Нет
end enum 


''' <summary>
'''Перечисление Тип задачи
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumVRTaskType'Тип задачи
  VRTaskType_Fonovay=2' Фоновая
  VRTaskType_K_opredelennomu_vremeni=1' К определенному времени
  VRTaskType_V_opredelenniy_moment=0' В определенный момент
end enum 


''' <summary>
'''Перечисление Для связи журналов друг с другом
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumJournalLinkType'Для связи журналов друг с другом
  JournalLinkType_Net=0' Нет
  JournalLinkType_Svyzka_ParentStructRowID__OPNv_peredlah_ob_ektaCLS=4' Связка ParentStructRowID  (в передлах объекта)
  JournalLinkType_Svyzka_InstanceID_OPNv_peredlah_ob_ektaCLS=3' Связка InstanceID (в передлах объекта)
  JournalLinkType_Ssilka_na_ob_ekt=1' Ссылка на объект
  JournalLinkType_Ssilka_na_stroku=2' Ссылка на строку
end enum 


''' <summary>
'''Перечисление Вариант уровня приложения, куда может генерироваться код
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumTargetType'Вариант уровня приложения, куда может генерироваться код
  TargetType_SUBD=0' СУБД
  TargetType_Dokumentaciy=3' Документация
  TargetType_MODEL_=1' МОДЕЛЬ
  TargetType_Prilogenie=2' Приложение
  TargetType_ARM=4' АРМ
end enum 


''' <summary>
'''Перечисление Тип надбавки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumAdditionType'Тип надбавки
  AdditionType_Plotnost_=2' Плотность
  AdditionType_Procee=3' Прочее
  AdditionType_Ves=0' Вес
  AdditionType_Ob_em=1' Объем
end enum 


''' <summary>
'''Перечисление Представление поля
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumPCB_LayerP'Представление поля
  PCB_LayerP_Ne_imeet_znaceniy=0' Не имеет значения
  PCB_LayerP_Pozitiv=1' Позитив
  PCB_LayerP_Negativ=2' Негатив
end enum 


''' <summary>
'''Перечисление Тип заказчика
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeClient'Тип заказчика
  typeClient_Razoviy=0' Разовый
end enum 


''' <summary>
'''Перечисление Тип кеширования для расчета прав на объекты
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumCacheType'Тип кеширования для расчета прав на объекты
  CacheType_Vse=2' Все
  CacheType_Tol_ko_svoi=0' Только свои
  CacheType_Podcinennie=1' Подчиненные
end enum 


''' <summary>
'''Перечисление Тип строки чека
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeRowCheque'Тип строки чека
  typeRowCheque_Prostoe_PO=4' Простое ПО
  typeRowCheque_dopDOTusluga_PO=2' доп.услуга ПО
  typeRowCheque_PO=1' ПО
  typeRowCheque_Procee=0' Прочее
  typeRowCheque_dopDOTusluga_zakaza=3' доп.услуга заказа
  typeRowCheque_Tovar=5' Товар
end enum 


''' <summary>
'''Перечисление Формат индикатора
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumMesureFormat'Формат индикатора
  MesureFormat_Cislo=0' Число
  MesureFormat_Data=1' Дата
  MesureFormat_Ob_ekt=4' Объект
  MesureFormat_Spravocnik=2' Справочник
  MesureFormat_Tekst=5' Текст
end enum 


''' <summary>
'''Перечисление Тип экспорта
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumExportType'Тип экспорта
  ExportType_Sayt_i_MB=3' Сайт и МБ
  ExportType_Sayt=1' Сайт
  ExportType_Net=0' Нет
end enum 


''' <summary>
'''Перечисление Тип шага процесса
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumWFStepClass'Тип шага процесса
  WFStepClass_PeriodicFunction=3' PeriodicFunction
  WFStepClass_SimpleFunction=0' SimpleFunction
  WFStepClass_StopFunction=2' StopFunction
  WFStepClass_StartFunction=1' StartFunction
end enum 


''' <summary>
'''Перечисление Тип шага в ХАССП процессе
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumHACCPStep'Тип шага в ХАССП процессе
  HACCPStep_Process=6' Процесс
  HACCPStep_Operaciy=3' Операция
  HACCPStep_Kontrol_nay_tocka=4' Контрольная точка
  HACCPStep_Zaversenie_processa=1' Завершение процесса
  HACCPStep_Ostanov_processa=2' Останов процесса
  HACCPStep_Korrektiruusee_deystvie=5' Корректирующее действие
  HACCPStep_Nacalo_processa=0' Начало процесса
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypePersonPay'
  typePersonPay_Polucatel_=1' Получатель
  typePersonPay_Otpravitel_=0' Отправитель
end enum 


''' <summary>
'''Перечисление День недели
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumDayInWeek'День недели
  DayInWeek_Cetverg=4' Четверг
  DayInWeek_Subbota=6' Суббота
  DayInWeek_Ponedel_nik=1' Понедельник
  DayInWeek_Voskresen_e=7' Воскресенье
  DayInWeek_Vtornik=2' Вторник
  DayInWeek_Pytnica=5' Пятница
  DayInWeek_Sreda=3' Среда
end enum 


''' <summary>
'''Перечисление GeneratorStyle
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumGeneratorStyle'GeneratorStyle
  GeneratorStyle_Odin_tip=0' Один тип
  GeneratorStyle_Vse_tipi_srazu=1' Все типы сразу
end enum 


''' <summary>
'''Перечисление Тип плательщика
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumPlatType'Тип плательщика
  PlatType_Polucatel_=1' Получатель
  PlatType_Otpravitel_=0' Отправитель
  PlatType_Drugoy=2' Другой
end enum 


''' <summary>
'''Перечисление Состояние заявки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enummsgState'Состояние заявки
  msgState_Soobseno_abonentu=1' Сообщено абоненту
  msgState_Promegutocniy_otvet=3' Промежуточный ответ
  msgState_Sostoynie_zayvki=0' Состояние заявки
  msgState_Abonent_ne_otvetil=2' Абонент не ответил
end enum 


''' <summary>
'''Перечисление Тип контрагента
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumKONTR_TYPE'Тип контрагента
  KONTR_TYPE_Procee=5' Прочее
  KONTR_TYPE_Ucreditel_=3' Учредитель
  KONTR_TYPE_Postavsik=1' Поставщик
  KONTR_TYPE_Partner=2' Партнер
  KONTR_TYPE_Perevozcik=4' Перевозчик
  KONTR_TYPE_Pokupatel_=0' Покупатель
end enum 


''' <summary>
'''Перечисление Тип номенкатуры
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeNomen'Тип номенкатуры
  typeNomen_Dokument=0' Документ
  typeNomen_Listovki=2' Листовки
  typeNomen_Tovar=1' Товар
  typeNomen_GazetiCOMA_gurnaliCOMA_brosuri=4' Газеты, журналы, брошюры
  typeNomen_Pis_mo=3' Письмо
end enum 


''' <summary>
'''Перечисление действие при открытии строки журнала
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumOnJournalRowClick'действие при открытии строки журнала
  OnJournalRowClick_Otkrit__dokument=2' Открыть документ
  OnJournalRowClick_Nicego_ne_delat_=0' Ничего не делать
  OnJournalRowClick_Otkrit__stroku=1' Открыть строку
end enum 


''' <summary>
'''Перечисление PartType
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumPartType'PartType
  PartType_Kollekciy=1' Коллекция
  PartType_Derevo=2' Дерево
  PartType_Stroka=0' Строка
  PartType_Rassirenie_s_dannimi=4' Расширение с данными
  PartType_Rassirenie=3' Расширение
end enum 


''' <summary>
'''Перечисление Сдельная  Ставка
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypePay'Сдельная  Ставка
  typePay_Sdel_nay=0' Сдельная
  typePay_Stavka=1' Ставка
end enum 


''' <summary>
'''Перечисление ReferenceType
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumReferenceType'ReferenceType
  ReferenceType_Na_istocnik_dannih=3' На источник данных
  ReferenceType_Skalyrnoe_pole_OPN_ne_ssilkaCLS=0' Скалярное поле ( не ссылка)
  ReferenceType_Na_stroku_razdela=2' На строку раздела
  ReferenceType_Na_ob_ekt_=1' На объект 
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumstateNomen'
  stateNomen_Dostavleno=4' Доставлено
  stateNomen_Prinyto=2' Принято
  stateNomen_V_processe=3' В процессе
  stateNomen_Pereadresaciy=6' Переадресация
  stateNomen_Oformlyetsy=0' Оформляется
  stateNomen_Vozvrat=5' Возврат
end enum 


''' <summary>
'''Перечисление Варианты условий
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumConditionType'Варианты условий
  ConditionType_LS=6' <
  ConditionType_GTEQ=4' >=
  ConditionType_LSEQ=7' <=
  ConditionType_none=0' none
  ConditionType_EQ=1' =
  ConditionType_like=8' like
  ConditionType_GT=3' >
  ConditionType_LSGT=2' <>
end enum 


''' <summary>
'''Перечисление Тип папки
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumFolderType'Тип папки
  FolderType_Udalennie=3' Удаленные
  FolderType_Vhodysie=1' Входящие
  FolderType_Otlogennie=9' Отложенные
  FolderType_Gurnal=4' Журнал
  FolderType_Ishodysie=2' Исходящие
  FolderType_Cernoviki=7' Черновики
  FolderType_Otpravlennie=6' Отправленные
  FolderType_V_rabote=8' В работе
  FolderType_Kalendar_=5' Календарь
  FolderType_Zaversennie=10' Завершенные
  FolderType_cls__=0' cls__
end enum 


''' <summary>
'''Перечисление Результат
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enummsgResult'Результат
  msgResult_Vipolneno=2' Выполнено
  msgResult_V_rabote=1' В работе
  msgResult_Rezul_tat=0' Результат
end enum 


''' <summary>
'''Перечисление Идентификация
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumHaccpIdent'Идентификация
  HaccpIdent_Cvet=1' Цвет
  HaccpIdent_Zapah=2' Запах
  HaccpIdent_Vnesniy_vid=0' Внешний вид
  HaccpIdent_Zvuk=5' Звук
  HaccpIdent_Vkus=4' Вкус
  HaccpIdent_Kacestvo_poverhnosti=6' Качество поверхности
  HaccpIdent_Forma=3' Форма
  HaccpIdent_Procee=100' Прочее
end enum 


''' <summary>
'''Перечисление Поведение при добавлении строки раздела
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumPartAddBehaivor'Поведение при добавлении строки раздела
  PartAddBehaivor_AddForm=0' AddForm
  PartAddBehaivor_RunAction=2' RunAction
  PartAddBehaivor_RefreshOnly=1' RefreshOnly
end enum 


''' <summary>
'''Перечисление Тип расширения
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumExtentionType'Тип расширения
  ExtentionType_VerifyRowExt=6' VerifyRowExt
  ExtentionType_CodeGenerator=7' CodeGenerator
  ExtentionType_DefaultExt=5' DefaultExt
  ExtentionType_StatusExt=0' StatusExt
  ExtentionType_JrnlRunExt=4' JrnlRunExt
  ExtentionType_CustomExt=2' CustomExt
  ExtentionType_ARMGenerator=8' ARMGenerator
  ExtentionType_OnFormExt=1' OnFormExt
  ExtentionType_JrnlAddExt=3' JrnlAddExt
end enum 


''' <summary>
'''Перечисление Договор  ТрудКнижка
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeCourier'Договор  ТрудКнижка
  typeCourier_Trudovay_knigka=1' Трудовая книжка
  typeCourier_Dogovor_podryda=0' Договор подряда
end enum 


''' <summary>
'''Перечисление Физическое лицо  Юрифическое лицо
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeFace'Физическое лицо  Юрифическое лицо
  typeFace_UrLico=1' ЮрЛицо
  typeFace_FizLico=0' ФизЛицо
end enum 


''' <summary>
'''Перечисление Тип контакта
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumContactStyle'Тип контакта
  ContactStyle_Procee=0' Прочее
  ContactStyle_Telefon=1' Телефон
  ContactStyle_EMNSMail=2' E-Mail
end enum 


''' <summary>
'''Перечисление Мужской / Женский
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumSex'Мужской / Женский
  Sex_Ne_susestvenno=0' Не существенно
  Sex_Mugskoy=1' Мужской
  Sex_Genskiy=-1' Женский
end enum 


''' <summary>
'''Перечисление Да / Нет (0 или 1)
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumYesNo'Да / Нет (0 или 1)
  YesNo_Da=1' Да
  YesNo_Net=0' Нет
end enum 


''' <summary>
'''Перечисление Вариант агрегации по полю
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumAggregationType'Вариант агрегации по полю
  AggregationType_SUM=3' SUM
  AggregationType_AVG=1' AVG
  AggregationType_CUSTOM=6' CUSTOM
  AggregationType_none=0' none
  AggregationType_COUNT=2' COUNT
  AggregationType_MAX=5' MAX
  AggregationType_MIN=4' MIN
end enum 


''' <summary>
'''Перечисление Состояние функции в бизнес процессе
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumWFFuncState'Состояние функции в бизнес процессе
  WFFuncState_Processed=8' Processed
  WFFuncState_InWork=3' InWork
  WFFuncState_Pause=4' Pause
  WFFuncState_InControl=6' InControl
  WFFuncState_Active=2' Active
  WFFuncState_Ready=5' Ready
  WFFuncState_Done=7' Done
  WFFuncState_Prepare=1' Prepare
  WFFuncState_Initial=0' Initial
end enum 


''' <summary>
'''Перечисление Занятость
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumEmployment'Занятость
  Employment_Casticnay=1' Частичная
  Employment_Ne_vagno=-1' Не важно
  Employment_Polnay=0' Полная
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumDeliveryOn'
  DeliveryOn_Megdunarodnay=0' Международная
  DeliveryOn_Strana=1' Страна
  DeliveryOn_Gorod=4' Город
  DeliveryOn_Strana2=2' Страна2
  DeliveryOn_Sub_ekt=3' Субъект
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeReceiv'
  typeReceiv_PTS=1' ПТС
  typeReceiv_SVS=0' СВС
end enum 


''' <summary>
'''Перечисление Да / Нет / Не определено
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumTriState'Да / Нет / Не определено
  TriState_Ne_susestvenno=-1' Не существенно
  TriState_Da=1' Да
  TriState_Net=0' Нет
end enum 


''' <summary>
'''Перечисление 
''' </summary>
''' <remarks>
'''
''' </remarks>
public enum enumtypeTariff'
  typeTariff_Usluga=1' Услуга
  typeTariff_Dostavka=0' Доставка
end enum 



''' <summary>
'''Реализация документа Карточка документа, то что раньше было режимом !
''' </summary>
''' <remarks>
'''Карточка
''' </remarks>
    Public Class Application
        Inherits BP3.Document.Doc_Base




''' <summary>
'''Название типа документа
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function MyTypeName() As String
            MyTypeName = "bp3card"
        End Function



''' <summary>
''' Внутренняя переменная для раздела Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
  Private m_bp3card_part As bp3card_part_col


''' <summary>
'''Свойство для доступа к строкам раздела Раздел
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public ReadOnly Property bp3card_part() As bp3card_part_col
            Get
                If m_bp3card_part Is Nothing Then
                    m_bp3card_part = New bp3card_part_col
                    m_bp3card_part.Application = Me
                    m_bp3card_part.Parent = Me
                    m_bp3card_part.Refresh()
                End If
                bp3card_part = m_bp3card_part
            End Get
        End Property


''' <summary>
''' Внутренняя переменная для раздела Поля карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
  Private m_bp3card_fld As bp3card_fld_col


''' <summary>
'''Свойство для доступа к строкам раздела Поля карточки
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public ReadOnly Property bp3card_fld() As bp3card_fld_col
            Get
                If m_bp3card_fld Is Nothing Then
                    m_bp3card_fld = New bp3card_fld_col
                    m_bp3card_fld.Application = Me
                    m_bp3card_fld.Parent = Me
                    m_bp3card_fld.Refresh()
                End If
                bp3card_fld = m_bp3card_fld
            End Get
        End Property


''' <summary>
''' Внутренняя переменная для раздела Карточка документа
''' </summary>
''' <remarks>
'''
''' </remarks>
  Private m_bp3card_def As bp3card_def_col


''' <summary>
'''Свойство для доступа к строкам раздела Карточка документа
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public ReadOnly Property bp3card_def() As bp3card_def_col
            Get
                If m_bp3card_def Is Nothing Then
                    m_bp3card_def = New bp3card_def_col
                    m_bp3card_def.Application = Me
                    m_bp3card_def.Parent = Me
                    m_bp3card_def.Refresh()
                End If
                bp3card_def = m_bp3card_def
            End Get
        End Property


''' <summary>
'''Количество разделов в объекте
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides ReadOnly Property CountOfParts() As Long
            Get
                CountOfParts = 3
            End Get
        End Property



''' <summary>
'''Получить раздел по номеру
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Function GetDocCollection_Base(ByVal Index As Long) As DocCollection_Base
            Select Case Index
         Case 1
            return bp3card_def
         Case 2
            return bp3card_part
         Case 3
            return bp3card_fld
            End Select
            return nothing
        End Function

        Public Overrides Sub Dispose()
            bp3card_def.Dispose()
            bp3card_part.Dispose()
            bp3card_fld.Dispose()
        End Sub



''' <summary>
'''Поиск элемента в коллекции
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Function FindInCollections(ByVal Table As String, ByVal InstID As String) As BP3.Document.DocRow_Base
        FindInCollections = Nothing
            dim mFindInCollections As BP3.Document.DocRow_Base
            mFindInCollections = bp3card_def.FindObject(Table, InstID)
            if not mFindInCollections is nothing then return mFindInCollections
            mFindInCollections = bp3card_part.FindObject(Table, InstID)
            if not mFindInCollections is nothing then return mFindInCollections
            mFindInCollections = bp3card_fld.FindObject(Table, InstID)
            if not mFindInCollections is nothing then return mFindInCollections
        End Function



''' <summary>
'''Загрузка данных из XML  файла
''' </summary>
''' <remarks>
'''
''' </remarks>
        Protected Overrides Sub XMLLoadCollections(ByVal node As System.Xml.XmlNode, Optional ByVal LoadMode As Integer = 0)
            Dim e_list As XmlNodeList
            On Error Resume Next
            e_list = node.SelectNodes("bp3card_def_COL")
            bp3card_def.XMLLoad(e_list, LoadMode)
            e_list = node.SelectNodes("bp3card_part_COL")
            bp3card_part.XMLLoad(e_list, LoadMode)
            e_list = node.SelectNodes("bp3card_fld_COL")
            bp3card_fld.XMLLoad(e_list, LoadMode)
        End Sub



''' <summary>
'''Сохранение данных в XML  файле
''' </summary>
''' <remarks>
'''
''' </remarks>
        Public Overrides Sub XMLSaveCollections(ByVal node As System.Xml.XmlElement, ByVal Xdom As System.Xml.XmlDocument)
            bp3card_def.XMLSave(node, Xdom)
            bp3card_part.XMLSave(node, Xdom)
            bp3card_fld.XMLSave(node, Xdom)
        End Sub



''' <summary>
'''Сохранение изменений в базе данных
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Overrides Sub BatchUpdate()
    bp3card_def.BatchUpdate
    bp3card_part.BatchUpdate
    bp3card_fld.BatchUpdate
End Sub
    End Class
End Namespace
