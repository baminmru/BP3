<?php
	 class C_users extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'Users.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'Users.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'usersid' =>  $this->input->get_post('usersid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'family' =>   $this->input->get_post('family', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'surname' =>   $this->input->get_post('surname', TRUE)
                ,'login' =>   $this->input->get_post('login', TRUE)
                ,'password' =>   $this->input->get_post('password', TRUE)
                ,'domainame' =>   $this->input->get_post('domainame', TRUE)
                ,'email' =>   $this->input->get_post('email', TRUE)
                ,'phone' =>   $this->input->get_post('phone', TRUE)
                ,'localphone' =>   $this->input->get_post('localphone', TRUE)
            );
            $users = $this->m_users->setRow($data);
            print json_encode($users);
    }
    function newRow() {
            log_message('debug', 'Users.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'usersid' =>  $this->input->get_post('usersid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'family' =>   $this->input->get_post('family', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'surname' =>   $this->input->get_post('surname', TRUE)
                ,'login' =>   $this->input->get_post('login', TRUE)
                ,'password' =>   $this->input->get_post('password', TRUE)
                ,'domainame' =>   $this->input->get_post('domainame', TRUE)
                ,'email' =>   $this->input->get_post('email', TRUE)
                ,'phone' =>   $this->input->get_post('phone', TRUE)
                ,'localphone' =>   $this->input->get_post('localphone', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $users= $this->m_users->newRow($instanceid,$data);
            $return = $users;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'Users.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $users = $this->m_users->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $users
            );
            print json_encode($return);
        }
    }
    function getRows() {
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'family', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $users= $this->m_users->getRowsByInstance($instanceid,$sort);
                }else{
                    $users= $this->m_users->getRows($sort);
                }
            }else{
              $users= $this->m_users->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $users
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'Users.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'family', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $users= $this->m_users->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $users
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need instanceid to query.'
            );
        }
        print json_encode($return);
    }
    function getRowsByParent() {
        log_message('debug', 'Users.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'family', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $users= $this->m_users->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $users
            );
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'Need parent parentid to query.'
            );
        }
        print json_encode($return);
    }
    function deleteRow() {
        log_message('debug', 'Users.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('usersid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_users->deleteRow($tempId);
        }
        else {
            $return = array(
                'success' => FALSE,
                'msg'     => 'No  ID to delete'
            );
        }
        print json_encode($return);
    }
    private function _loadModels () {
        $this->load->model('M_users', 'm_users');
    }
}
?>