
Ext.require([
'Ext.form.*'
]);
  bp3app_= null;
function bp3app_Panel_(objectID, RootPanel, selection){ 


    var store_bp3app_menu = Ext.create('Ext.data.Store', {
        model:'model_bp3app_menu',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_menu/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3app_info = Ext.create('Ext.data.Store', {
        model:'model_bp3app_info',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_info/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3app_modules = Ext.create('Ext.data.Store', {
        model:'model_bp3app_modules',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_modules/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });

    var store_bp3app_oper = Ext.create('Ext.data.Store', {
        model:'model_bp3app_oper',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_oper/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        }
    });

    var store_bp3app_rigthtype = Ext.create('Ext.data.Store', {
        model:'model_bp3app_rigthtype',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3app_rigthtype/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
                },
            extraParams:{
                instanceid: objectID
            }
        }
    });
          DefineForms_bp3app_menu_();
          DefineForms_bp3app_info_();
          DefineForms_bp3app_modules_();
          DefineForms_bp3app_rigthtype_();
     var   int_bp3app_menu_     =      DefineInterface_bp3app_menu_('int_bp3app_menu',store_bp3app_menu);
     var   int_bp3app_info_     =      DefineInterface_bp3app_info_('int_bp3app_info',store_bp3app_info, selection);
     var   int_bp3app_modules_     =      DefineInterface_bp3app_modules_('int_bp3app_modules',store_bp3app_modules);
     var   int_bp3app_rigthtype_     =      DefineInterface_bp3app_rigthtype_('int_bp3app_rigthtype',store_bp3app_rigthtype);
     bp3app_= Ext.create('Ext.form.Panel', {
      id: 'bp3app',
      layout:'fit',
      fieldDefaults: {
          labelAlign:             'top',
          msgTarget:              'side'
        },
        defaults: {
        anchor:'100%'
        },

        instanceid:objectID,
                onCommit: function(){
        		},
        		onButtonOk: function()
        		{
        			var me = this;
        	    int_bp3app_info_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3app_info_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3app',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Меню',
            layout:'fit',
            itemId:'tab_bp3app_menu',
            items:[ // panel on tab 
int_bp3app_menu_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Описание',
            layout:'fit',
            itemId:'tab_bp3app_info',
            items:[ // panel on tab 
int_bp3app_info_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Модули',
            layout:'fit',
            itemId:'tab_bp3app_modules',
            items:[ // panel on tab 
int_bp3app_modules_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Тип права',
            layout:'fit',
            itemId:'tab_bp3app_rigthtype',
            items:[ // panel on tab 
int_bp3app_rigthtype_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3app_.closable= true;
       bp3app_.title= 'Приложение';
       bp3app_.frame= true;
    }else{
       bp3app_.closable= false;
       bp3app_.title= '';
       bp3app_.frame= false;
    }
   int_bp3app_menu_.instanceid=objectID;	
       store_bp3app_menu.load(  {params:{ instanceid:objectID} } );
   store_bp3app_info.on('load', function() {
        if(store_bp3app_info.count()==0){
            store_bp3app_info.insert(0, new model_bp3app_info());
        }
        record= store_bp3app_info.getAt(0);
        record['instanceid']=objectID;
   int_bp3app_info_.setActiveRecord(record,objectID);	
   });
       store_bp3app_info.load( {params:{ instanceid:objectID} }  );
   int_bp3app_modules_.instanceid=objectID;	
       store_bp3app_modules.load(  {params:{ instanceid:objectID} } );
   int_bp3app_rigthtype_.instanceid=objectID;	
       store_bp3app_rigthtype.load(  {params:{ instanceid:objectID} } );
    return bp3app_;
}