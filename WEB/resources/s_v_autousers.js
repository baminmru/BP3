
 Ext.define('model_v_autousers',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'users_phone', type: 'string'}
            ,{name:'users_localphone', type: 'string'}
            ,{name:'users_domainame', type: 'string'}
            ,{name:'users_login', type: 'string'}
            ,{name:'users_surname', type: 'string'}
            ,{name:'users_family', type: 'string'}
            ,{name:'users_email', type: 'string'}
            ,{name:'users_name', type: 'string'}
        ]
    });

    var store_v_autousers = Ext.create('Ext.data.Store', {
        model:'model_v_autousers',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autousers/getRows',
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