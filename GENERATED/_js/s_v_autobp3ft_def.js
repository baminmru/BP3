﻿
 Ext.define('model_v_autobp3ft_def',{
            extend:'Ext.data.Model',
        fields: [
            {name: 'instanceid',type: 'string'}
            ,{name: 'id',type: 'string'}
            ,{name:'bp3ft_def_maximum', type: 'string'}
            ,{name:'bp3ft_def_gridsorttype', type: 'string'}
            ,{name:'bp3ft_def_delayedsave', type: 'string'}
            ,{name:'bp3ft_def_name', type: 'string'}
            ,{name:'bp3ft_def_minimum', type: 'string'}
            ,{name:'bp3ft_def_allowsize', type: 'string'}
            ,{name:'bp3ft_def_typestyle', type: 'string'}
            ,{name:'bp3ft_def_allowlikesearch', type: 'string'}
            ,{name:'bp3ft_def_the_comment', type: 'string'}
        ]
    });

    var store_v_autobp3ft_def = Ext.create('Ext.data.Store', {
        model:'model_v_autobp3ft_def',
        autoLoad: false,
        autoSync: false,
        remoteSort: true,
        remoteFilter:true,
        pageSize : 30,
        proxy: {
            type:   'ajax',
                url:   rootURL+'index.php/c_v_autobp3ft_def/getRows',
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