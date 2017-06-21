
Ext.require([
'Ext.form.*'
]);
  mtzusers_= null;
function MTZUsers_Panel_(objectID, RootPanel, selection){ 


    var store_users = Ext.create('Ext.data.Store', {
        model:'model_users',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_users/getRows',
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

    var store_groups = Ext.create('Ext.data.Store', {
        model:'model_groups',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_groups/getRows',
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

    var store_groupuser = Ext.create('Ext.data.Store', {
        model:'model_groupuser',
        autoLoad: false,
        autoSync: false,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_groupuser/getRows',
            reader: {
                type:   'json'
                ,root:  'data'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            }
        }
    });
          DefineForms_users_();
          DefineForms_groups_();
     var   int_users_     =      DefineInterface_users_('int_users',store_users);
     var   int_groups_     =      DefineInterface_groups_('int_groups',store_groups);
     mtzusers_= Ext.create('Ext.form.Panel', {
      id: 'mtzusers',
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
        		},
        		onButtonCancel: function()
        		{
        		},
        canClose: function(){
        	return true;
        },
        items: [{
            xtype:'tabpanel',
            itemId:'tabs_mtzusers',
            activeTab: 0,
            layout: 'fit',
            tabPosition:'top',
            border:0,
            items:[   // tabs
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Пользователи',
            layout:'fit',
            itemId:'tab_users',
            items:[ // panel on tab 
int_users_
        ] // panel on tab  form or grid
      } // end tab
,
            { // begin part tab
            xtype:'panel',
            border:0,
            title: 'Группы',
            layout:'fit',
            itemId:'tab_groups',
            items:[ // panel on tab 
int_groups_
        ] // panel on tab  form or grid
      } // end tab
    ] // part tabs
    }] // tabpanel
    }); //Ext.Create
    if(RootPanel==true){
       mtzusers_.closable= true;
       mtzusers_.title= 'Справочник: пользователи';
       mtzusers_.frame= true;
    }else{
       mtzusers_.closable= false;
       mtzusers_.title= '';
       mtzusers_.frame= false;
    }
   int_users_.instanceid=objectID;	
       store_users.load(  {params:{ instanceid:objectID} } );
   int_groups_.instanceid=objectID;	
       store_groups.load(  {params:{ instanceid:objectID} } );
    return mtzusers_;
}