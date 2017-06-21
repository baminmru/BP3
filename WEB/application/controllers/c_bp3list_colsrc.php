<?php
	 class C_bp3list_colsrc extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3list_colsrc.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3list_colsrc.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3list_colsrcid' =>  $this->input->get_post('bp3list_colsrcid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'viewfield' =>   $this->input->get_post('viewfield', TRUE)
            );
            $bp3list_colsrc = $this->m_bp3list_colsrc->setRow($data);
            print json_encode($bp3list_colsrc);
    }
    function newRow() {
            log_message('debug', 'bp3list_colsrc.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3list_colsrcid' =>  $this->input->get_post('bp3list_colsrcid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'viewfield' =>   $this->input->get_post('viewfield', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3list_colsrc= $this->m_bp3list_colsrc->newRow($instanceid,$parentid,$data);
            $return = $bp3list_colsrc;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3list_colsrc.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3list_colsrc = $this->m_bp3list_colsrc->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3list_colsrc
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
            	$sort[] = array('property'=>'viewfield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
            $parentid=$this->input->get('parentid', FALSE);
            if(isset($parentid)){
                if($parentid!=''){
                    $bp3list_colsrc= $this->m_bp3list_colsrc->getRowsByParent($parentid,$sort);
                }else{
                    $bp3list_colsrc= $this->m_bp3list_colsrc->getRows($sort);
                }
            }else{
              $bp3list_colsrc= $this->m_bp3list_colsrc->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_colsrc
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3list_colsrc.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'viewfield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $InstId  =  $this->input->get_post('instanceid', TRUE);
        if (strlen($InstId) > 0) {
            $bp3list_colsrc= $this->m_bp3list_colsrc->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_colsrc
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
        log_message('debug', 'bp3list_colsrc.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
            $group = $this->input->get('group', FALSE); 
           $sort = $this->input->get('sort', FALSE);
           if(!$sort && $group) $sort = $group;
           if(!$sort || $group == $sort) 
            {
            	$sort = json_decode($sort);
            	$sort[] = array('property'=>'viewfield', 'direction'=>'ASC');
            	$sort = json_encode($sort);
            }
        $ParentId  =  $this->input->get_post('parentid', TRUE);
        if (strlen($ParentId) > 0) {
            $bp3list_colsrc= $this->m_bp3list_colsrc->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3list_colsrc
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
        log_message('debug', 'bp3list_colsrc.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3list_colsrcid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3list_colsrc->deleteRow($tempId);
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
        $this->load->model('M_bp3list_colsrc', 'm_bp3list_colsrc');
    }
}
?>