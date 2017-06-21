var login_win=Ext.create('Ext.Window', 
{
	title:"Авторизация",
	itemId:"login_win",
	layout:"fit",
	constrainHeader:true,
	width:300,
//	height:180,
	closable:false,
	modal:true,
	resizable:false,
	plain:true,
	border:false,
	items:
	[
		{
			xtype: 'form',			
			url:"data.php/authorization/get",
			frame:true,
			height:"100%",
			title:"",
			defaultType:"textfield",
			monitorValid:true,
			defaults:
			{
				allowBlank:false,
				labelWidth:120,
				labelAlign:'right',
				labelStyle:'font-weight: bold'
			},
			items:
			[
				{
					fieldLabel:"Пользователь",
					itemId:"login",
					name:"login",
					listeners:
					{
						afterrender: function(field)
						{
							field.focus(false,1000);
						},
						specialkey:function(b,a)
						{
							if(a.getKey()==a.ENTER)
							{
								b.up('form').up('window').doLogin();
							}
						}
					}
				},
				{
					fieldLabel:"Пароль",
					itemId:"password",
					name:"password",
					inputType:"password",
					listeners:
					{
						specialkey:function(b,a)
						{
							if(a.getKey()==a.ENTER)
							{
								b.up('form').up('window').doLogin();
							}
						}
					}
				},
				{
					fieldLabel:"Новый пароль",
					itemId:"passwordNew",
					name:"passwordNew",
					inputType:"password",
					hidden: true,
					disabled: true,
					listeners:
					{
						specialkey:function(b,a)
						{
							if(a.getKey()==a.ENTER)
							{
								b.up('form').up('window').doLogin();
							}
						}
					}
				},
				{
					fieldLabel:"Подтверждение",
					itemId:"passwordConfirm",
					name:"passwordConfirm",
					inputType:"password",
					submitValue: false,
					hidden: true,
					disabled: true,
					listeners:
					{
						specialkey:function(b,a)
						{
							if(a.getKey()==a.ENTER)
							{
								b.up('form').up('window').doLogin();
							}
						}
					}
				},
				{
					xtype:'label',
					itemId:'passMessage',
					html:'<div style="border:1px solid #ff5555; background-color:#ffdddd; padding:3px; text-align:justify;">Ваш аккаунт использует пароль по умолчанию, который не соответствует требованиям безопасности.<br>Пожалуйста, создайте новый пароль.</div>',
					hidden:true
				}
			]
		}
	],

	buttons:
	[		
		{
			text:"Войти",
			icon:"icons/key_go.png",
//			iconCls:"icon-key_go",
			handler: function(b)
			{
				b.up('window').doLogin();
			}
		}
	],
	doLogin: function()
	{		
		var me = this;
		
		var form = me.down('form').getForm();
		
		if(form.isValid())
		{
			if(me.down('form').down('#passwordNew').getValue() != me.down('form').down('#passwordConfirm').getValue()) {
				Ext.Msg.alert("Ошибка авторизации!","Новый пароль не совпадает с подтверждением");
			}
			else {
				form.submit(
				{
					method:"POST",
					waitTitle:"Соединение", 
					waitMsg:"Отсылка данных...",
					success:function()
					{
							window.location = './';
							me.close();
					},
					failure:function(c,b)
					{
						if(b.result.msg == "changepassword") {
							form.url="data.php/changepassword/get";
							me.down('form').down('#passwordNew').show();
							me.down('form').down('#passwordConfirm').show();
							me.down('form').down('#passMessage').show();
							me.down('form').down('#passwordNew').enable();
							me.down('form').down('#passwordConfirm').enable();
						}
						else {
							if(b.failureType=="server") Ext.Msg.alert("Ошибка авторизации!", b.result.msg);
							else Ext.Msg.alert("Внимание!","Сервер недоступен : "+b.response.responseText);
						}
					}
				})
			}
		}
	}
});