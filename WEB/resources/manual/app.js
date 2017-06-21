
Ext.Loader.setConfig({
  enabled: true
 });
 Ext.Loader.setPath('Ext.ux', rootURL+'ux');

 
 Ext.require([
	'Ext.grid.*',
	'Ext.data.*',
	'Ext.util.*',
	'Ext.tab.*',
	'Ext.button.*',
	'Ext.form.*',
	'Ext.state.*',
	'Ext.layout.*',
	'Ext.Action',
	'Ext.resizer.Splitter',
	'Ext.fx.target.Element',
	'Ext.fx.target.Component',
	'Ext.window.Window',
	'Ext.selection.CellModel',
	'Ext.toolbar.Paging',
	'Ext.ModelManager',
	'Ext.tip.QuickTipManager',
	'Ext.ux.statusbar.StatusBar',
	'Ext.ux.CheckColumn', 
	'Ext.ux.grid.FiltersFeature',
	'Ext.ux.PreviewPlugin'
 ]);
 
 Ext.Loader.setConfig({enabled: true});

var menuPanel;
var leftPanel;
var contentPanel;
var stateFulSystem=false;


Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    
    // setup the state provider, all state information will be saved to a cookie

	try{
		Ext.state.Manager.setProvider(Ext.create('Ext.state.LocalStorageProvider'));
		stateFulSystem=true;
	}catch( ex ){
		//alert(ex);
		stateFulSystem=false;

	}

    UserLogin();
});


function EnableActions(){

	app_actions.each(function(record,idx){
	
	 var name=record.get('menucode'); 
	 var enableMenu = record.get('accesible'); 
	 var comp=null;
	 //console.log('code->'+name);
	 if(enableMenu==-1){
		comp=null;
		//startMenu.items[0].menu.down("#"+name)
		comp=menuPanel.down("#"+name);
		//console.log('comp->'+comp);
		if (comp!=null){
			comp.hidden=false;
			comp.disabled=false;
		    /* var comp1=null
			 comp1=comp.up();
			if(comp1!=null)
				if(comp1.hidden)
					comp1.show(); */
		}
	 }else{
		comp=null;
		comp=menuPanel.down("#"+name);
		//console.log('comp->'+comp);
		if (comp!=null){
			comp.disabled=true;
			comp.hidden=true;
			
		}
		
	 }
	 
	}
	
	
	);
	menuPanel.doLayout();
	

	PrepareRoles();
	
	
};


function MakeExit(btn){
	if(btn=="yes"){
		Ext.Ajax.request(
				{
					url: rootURL+'index.php/app/logout',
					method:  'POST',
					success: function(response){
						document.location=document.location;
						
					}
				}
			);
		//document.location=document.location;
	}
};
var actionEXIT = Ext.create('Ext.Action', {
	itemId:'actionEXIT',
	text: 'Выход',
	iconCls: 'icon-door',
	disabled:false,
	handler: function(){
		Ext.Msg.confirm('Выход из приложения?',
			'Завершить работу с приложением?',
			 MakeExit);
		
	}
});

var actionChangePassword = Ext.create('Ext.Action', {
	itemId:'actionChangePassword',
	text: 'Сменить пароль',
	iconCls: 'icon-building_key',
	disabled:false,
	handler: function() {
			var edit = Ext.create('EditWindow_sp_password');
			edit.show();
		}
});






function MyInit(){
    combo_pbar = Ext.create('Ext.ProgressBar', {
        id:'combo_pbar',
        width:300,
        renderTo:'loading'
    });

	
    var app_info_loaded=false;
	app_info = Ext.create('Ext.data.Store', {
        model:'application_info',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/app/getSessionInfo',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        },
       listeners: {
       'load': function(){
		app_info_loaded =true;
		combo_StoreLoaded=true; 
		var comp=menuPanel.down("#sessionInfo"); 
	    comp.setValue('Пользователь: '+app_info.getAt(0).get("info") + '. (' + app_info.getAt(0).get("rolename") +')' );
	   }
       }
    });
	combo_Stores.push(app_info);
	

	app_modes = Ext.create('Ext.data.Store', {
        model:'application_modes',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/app/getModes',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        },
       listeners: {
       'load': function(){ combo_StoreLoaded=true; }
       }
    });
	combo_Stores.push(app_modes);

	app_actions = Ext.create('Ext.data.Store', {
        model:'application_actions',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   'index.php/app/getActions',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        },
       listeners: {
       'load': function(){app_actions_loaded =true;combo_StoreLoaded=true; EnableActions();}
       }
    });
	combo_Stores.push(app_actions);
	
	
	
	

    menuPanel = new Ext.panel.Panel({
        xtype:'panel',
        region:'north',
        dockedItems:{
            itemId:'toolbar',
            xtype:'toolbar',
            items:[ 
                {
					itemId:'actionFile',
				    text:'Файл',
                    iconCls:'icon-folder',
                    menu:[ actionChangePassword,actionEXIT] 
				
				}
				,
				
				/*{
					itemId:'actionKey',
				    text:'Настройки',
                    iconCls:'icon-book_key',
                    menu:[actioniu_d_urole,actioniu_int,actioniu_rcfg,actioniu_u] 
				
				}
				,*/
				
				{
					itemId:'actionDict',
				    text:'Моделирование',
                    iconCls:'icon-book_open',
                    menu:[actionbp3dic,actionbp3ft ,actionbp3doc,actionbp3qry,actionbp3list ,actionbp3card ,actionbp3report , actionbp3app] 
				
				}
			
				,'->',
				{
					itemId:'sessionInfo',
					xtype:'displayfield',
					iconCls:'icon-information'//,
					//menu:[ actionInfo]
				}
			
				
            ]
        }
    });
	
	
    contentPanel = new Ext.tab.Panel({
        region:'center',
        xtype:'tabpanel', // TabPanel itself has no title
		splitter:true,
        activeTab:0      // First tab active by default
    });

	/*statusPanel = new Ext.Panel( {
	 region:'south',
     hideHeaders:true,
	 title:'',
	 //layout:'hbox',
	 border:false,
	 bbar:
		 Ext.create('Ext.ux.StatusBar', {
			id: 'my-status',
			region:'south',
			// defaults to use when the status is cleared:
			defaultText: 'все хорошо',
			//defaultIconCls: 'icon-bullet_green',

			// values to set initially:
			text: 'Готово',
			//iconCls: 'icon-bullet_green',
			width:600

		})
	
	});
	*/
	
	statusPanel =	 Ext.create('Ext.ux.StatusBar', {
			id: 'my-status',
			region:'south',
			// defaults to use when the status is cleared:
			defaultText: 'все хорошо',
			//defaultIconCls: 'icon-bullet_green',
			layout:'fit',
			// values to set initially:
			text: 'Готово',
			height:33

		});
	

    var vPort = new Ext.container.Viewport({
            layout:'border',
            renderTo:Ext.getBody(),
            items:[ /*leftPanel,*/ menuPanel, 
                contentPanel,statusPanel]
        }
    );

	combo_LoadNext();
	
	//setInterval(function() { app_info.load() }, 60000);
	
}




var defaultMenuDisabled=false;
var defaultMenuHidden=false;
//////////////////////////////////////////////////////