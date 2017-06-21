
var actionbp3app = Ext.create('Ext.Action', {
    itemId:             'actionbp3app',
    text:               'Приложение',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3app_jrnl');
			if(j==null){
				j=bp3app_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3card = Ext.create('Ext.Action', {
    itemId:             'actionbp3card',
    text:               'Карточка',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3card_jrnl');
			if(j==null){
				j=bp3card_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3dic = Ext.create('Ext.Action', {
    itemId:             'actionbp3dic',
    text:               'Справочники моделлера',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3dic_jrnl');
			if(j==null){
				j=bp3dic_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3doc = Ext.create('Ext.Action', {
    itemId:             'actionbp3doc',
    text:               'Документ',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3doc_jrnl');
			if(j==null){
				j=bp3doc_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3ft = Ext.create('Ext.Action', {
    itemId:             'actionbp3ft',
    text:               'Типы полей',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3ft_jrnl');
			if(j==null){
				j=bp3ft_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3list = Ext.create('Ext.Action', {
    itemId:             'actionbp3list',
    text:               'Журнал документов',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3list_jrnl');
			if(j==null){
				j=bp3list_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3qry = Ext.create('Ext.Action', {
    itemId:             'actionbp3qry',
    text:               'Запрос',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3qry_jrnl');
			if(j==null){
				j=bp3qry_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3report = Ext.create('Ext.Action', {
    itemId:             'actionbp3report',
    text:               'Отчет',
    iconCls:            'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
             handler: function(){
			var j=Ext.getCmp('bp3report_jrnl');
			if(j==null){
				j=bp3report_Jrnl();
				j.iconCls='icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
             }
});
var actionbp3dic = Ext.create('Ext.Action', {
        itemId:  'actionbp3dic',
        text:   'Справочники моделлера',
        iconCls:  'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
			handler: function(){
			var j=Ext.getCmp('bp3dic');
			if(j==null){
				j=eval('bp3dic_Panel_'+OTEditMode('bp3dic')+'(\'{F234FB36-F7CF-4D4E-A097-CAFB97D2F9AB}\', true)');
        j.iconCls=  'icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
        }
    });
var actionMTZ2JOB = Ext.create('Ext.Action', {
        itemId:  'actionMTZ2JOB',
        text:   'Отложенные обработки',
        iconCls:  'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
			handler: function(){
			var j=Ext.getCmp('mtz2job');
			if(j==null){
				j=eval('MTZ2JOB_Panel_'+OTEditMode('MTZ2JOB')+'(\'{290835AD-FDD6-413D-BEF9-9A144F1C6220}\', true)');
        j.iconCls=  'icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
        }
    });
var actionMTZMetaModel = Ext.create('Ext.Action', {
        itemId:  'actionMTZMetaModel',
        text:   'Спец.: Метамодель системы',
        iconCls:  'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
			handler: function(){
			var j=Ext.getCmp('mtzmetamodel');
			if(j==null){
				j=eval('MTZMetaModel_Panel_'+OTEditMode('MTZMetaModel')+'(\'{88DEEBA4-69B1-454A-992A-FAE3CEBFBCA1}\', true)');
        j.iconCls=  'icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
        }
    });
var actionMTZSystem = Ext.create('Ext.Action', {
        itemId:  'actionMTZSystem',
        text:   'Спец.: Системные данные',
        iconCls:  'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
			handler: function(){
			var j=Ext.getCmp('mtzsystem');
			if(j==null){
				j=eval('MTZSystem_Panel_'+OTEditMode('MTZSystem')+'(\'{C5A874A1-1D01-43F5-AA2B-5431031FD45C}\', true)');
        j.iconCls=  'icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
        }
    });
var actionMTZUsers = Ext.create('Ext.Action', {
        itemId:  'actionMTZUsers',
        text:   'Справочник: пользователи',
        iconCls:  'icon-brick',
			 disabled:defaultMenuDisabled,
			 hidden:defaultMenuHidden,
			handler: function(){
			var j=Ext.getCmp('mtzusers');
			if(j==null){
				j=eval('MTZUsers_Panel_'+OTEditMode('MTZUsers')+'(\'{E0FB5E85-050E-4322-8505-9E0CA132E901}\', true)');
        j.iconCls=  'icon-brick';
				contentPanel.add(j);
				contentPanel.setActiveTab(j);
			}
			else{
				contentPanel.setActiveTab(j);
			}
        }
    });