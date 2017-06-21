
 Ext.define('model_v_autobp3qry_def',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'bp3qry_def_forchoose', type: 'string'}
            ,{name:'bp3qry_def_srcpart', type: 'string'}
            ,{name:'bp3qry_def_the_alias', type: 'string'}
            ,{name:'bp3qry_def_name', type: 'string'}
        ]
    });

    var store_v_autobp3qry_def = Ext.create('Ext.data.Store', {
        model:'model_v_autobp3qry_def',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autobp3qry_def/getRows',
            reader: {
                type:   'json'
                ,root:  'rows'
                ,totalProperty: 'total'
                ,successProperty:  'success'
                ,messageProperty:  'msg'
            },
            listeners: {
                exception: function(proxy, response, operation){
                    Ext.MessageBox.show({
                        title: 'REMOTE EXCEPTION',
                        msg:    operation.getError(),
                        icon : Ext.MessageBox.ERROR,
                        buttons : Ext.Msg.OK
                    });
                }
            }
        }
    });