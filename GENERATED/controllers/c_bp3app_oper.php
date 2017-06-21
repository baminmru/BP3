<?php
	 class C_bp3app_oper extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'BP3APP_OPER.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'BP3APP_OPER.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3app_operid' =>  $this->input->get_post('bp3app_operid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'righttype' =>   $this->input->get_post('righttype', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
                ,'isreport' =>   $this->input->get_post('isreport', TRUE)
            );
            $bp3app_oper = $this->m_bp3app_oper->setRow($data);
            print json_encode($bp3app_oper);
    }
    function newRow() {
            log_message('debug', 'BP3APP_OPER.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3app_operid' =>  $this->input->get_post('bp3app_operid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'righttype' =>   $this->input->get_post('righttype', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'theicon' =>   $this->input->get_post('theicon', TRUE)
                ,'isreport' =>   $this->input->get_post('isreport', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3app_oper= $this->m_bp3app_oper->newRow($instanceid,$parentid,$data);
            $return = $bp3app_oper;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'BP3APP_OPER.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3app_oper = $this->m_bp3app_oper->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3app_oper
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
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $bp3app_oper= $this->m_bp3app_oper->getRowsByParent($parentid,$sort);
                }else{
                    $bp3app_oper= $this->m_bp3app_oper->getRows($sort);
                }
            }else{
              $bp3app_oper= $this->m_bp3app_oper->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_oper
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'BP3APP_OPER.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3app_oper= $this->m_bp3app_oper->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3app_oper
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
        log_message('debug', 'BP3APP_OPER.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'sequence', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3app_oper= $this->m_bp3app_oper->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3app_oper
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
        log_message('debug', 'BP3APP_OPER.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3app_operid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3app_oper->deleteRow($tempId);
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
        $this->load->model('M_bp3app_oper', 'm_bp3app_oper');
    }
}
?>