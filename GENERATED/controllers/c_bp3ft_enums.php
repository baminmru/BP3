<?php
	 class C_bp3ft_enums extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3ft_enums.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3ft_enums.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3ft_enumsid' =>  $this->input->get_post('bp3ft_enumsid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'nameincode' =>   $this->input->get_post('nameincode', TRUE)
                ,'namevalue' =>   $this->input->get_post('namevalue', TRUE)
            );
            $bp3ft_enums = $this->m_bp3ft_enums->setRow($data);
            print json_encode($bp3ft_enums);
    }
    function newRow() {
            log_message('debug', 'bp3ft_enums.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3ft_enumsid' =>  $this->input->get_post('bp3ft_enumsid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'nameincode' =>   $this->input->get_post('nameincode', TRUE)
                ,'namevalue' =>   $this->input->get_post('namevalue', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3ft_enums= $this->m_bp3ft_enums->newRow($instanceid,$data);
            $return = $bp3ft_enums;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3ft_enums.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3ft_enums = $this->m_bp3ft_enums->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3ft_enums
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
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3ft_enums= $this->m_bp3ft_enums->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3ft_enums= $this->m_bp3ft_enums->getRows($sort);
                }
            }else{
              $bp3ft_enums= $this->m_bp3ft_enums->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3ft_enums
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3ft_enums.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3ft_enums= $this->m_bp3ft_enums->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3ft_enums
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
        log_message('debug', 'bp3ft_enums.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'name', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3ft_enums= $this->m_bp3ft_enums->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3ft_enums
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
        log_message('debug', 'bp3ft_enums.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3ft_enumsid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3ft_enums->deleteRow($tempId);
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
        $this->load->model('M_bp3ft_enums', 'm_bp3ft_enums');
    }
}
?>