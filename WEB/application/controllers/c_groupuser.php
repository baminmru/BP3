<?php
	 class C_groupuser extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'GroupUser.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'GroupUser.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'groupuserid' =>  $this->input->get_post('groupuserid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'theuser' =>   $this->input->get_post('theuser', TRUE)
            );
            $groupuser = $this->m_groupuser->setRow($data);
            print json_encode($groupuser);
    }
    function newRow() {
            log_message('debug', 'GroupUser.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'groupuserid' =>  $this->input->get_post('groupuserid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'theuser' =>   $this->input->get_post('theuser', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $groupuser= $this->m_groupuser->newRow($instanceid,$parentid,$data);
            $return = $groupuser;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'GroupUser.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $groupuser = $this->m_groupuser->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $groupuser
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
            	$sort[] = array('property'=>'theuser', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $groupuser= $this->m_groupuser->getRowsByParent($parentid,$sort);
                }else{
                    $groupuser= $this->m_groupuser->getRows($sort);
                }
            }else{
              $groupuser= $this->m_groupuser->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $groupuser
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'GroupUser.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'theuser', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $groupuser= $this->m_groupuser->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $groupuser
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
        log_message('debug', 'GroupUser.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'theuser', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $groupuser= $this->m_groupuser->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $groupuser
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
        log_message('debug', 'GroupUser.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('groupuserid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_groupuser->deleteRow($tempId);
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
        $this->load->model('M_groupuser', 'm_groupuser');
    }
}
?>