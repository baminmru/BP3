
Ext.require([
'Ext.form.*'
]);
  bp3ft_= null;
function bp3ft_Panel_(objectID, RootPanel, selection){ 


    var store_bp3ft_def = Ext.create('Ext.data.Store', {
        model:'model_bp3ft_def',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3ft_def/getRows',
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

    var store_bp3ft_map = Ext.create('Ext.data.Store', {
        model:'model_bp3ft_map',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3ft_map/getRows',
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

    var store_bp3ft_enums = Ext.create('Ext.data.Store', {
        model:'model_bp3ft_enums',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_bp3ft_enums/getRows',
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
          DefineForms_bp3ft_def_();
          DefineForms_bp3ft_map_();
          DefineForms_bp3ft_enums_();
     var   int_bp3ft_def_     =      DefineInterface_bp3ft_def_('int_bp3ft_def',store_bp3ft_def, selection);
     var   int_bp3ft_map_     =      DefineInterface_bp3ft_map_('int_bp3ft_map',store_bp3ft_map);
     var   int_bp3ft_enums_     =      DefineInterface_bp3ft_enums_('int_bp3ft_enums',store_bp3ft_enums);
     bp3ft_= Ext.create('Ext.form.Panel', {
      id: 'bp3ft',
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
        	    int_bp3ft_def_.doSave( me.onCommit);
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return int_bp3ft_def_.canClose();
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_bp3ft',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Тип поля',
            layout:'fit',
            itemId:'tab_bp3ft_def',
            items:[ // panel on tab 
int_bp3ft_def_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Отображение',
            layout:'fit',
            itemId:'tab_bp3ft_map',
            items:[ // panel on tab 
int_bp3ft_map_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Зачения',
            layout:'fit',
            itemId:'tab_bp3ft_enums',
            items:[ // panel on tab 
int_bp3ft_enums_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       bp3ft_.closable= true;
       bp3ft_.title= 'Типы полей';
       bp3ft_.frame= true;
    }else{
       bp3ft_.closable= false;
       bp3ft_.title= '';
       bp3ft_.frame= false;
    }
   store_bp3ft_def.on('load', function() {
        if(store_bp3ft_def.count()==0){
            store_bp3ft_def.insert(0, new model_bp3ft_def());
        }
        record= store_bp3ft_def.getAt(0);
        record['instanceid']=objectID;
   int_bp3ft_def_.setActiveRecord(record,objectID);	
   });
       store_bp3ft_def.load( {params:{ instanceid:objectID} }  );
   int_bp3ft_map_.instanceid=objectID;	
       store_bp3ft_map.load(  {params:{ instanceid:objectID} } );
   int_bp3ft_enums_.instanceid=objectID;	
       store_bp3ft_enums.load(  {params:{ instanceid:objectID} } );
    return bp3ft_;
}