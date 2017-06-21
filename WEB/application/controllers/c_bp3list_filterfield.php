<?php
	 class C_bp3list_filterfield extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3list_filterfield.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3list_filterfield.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3list_filterfieldid' =>  $this->input->get_post('bp3list_filterfieldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'fieldsize' =>   $this->input->get_post('fieldsize', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'valuearray' =>   $this->input->get_post('valuearray', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
            );
            $bp3list_filterfield = $this->m_bp3list_filterfield->setRow($data);
            print json_encode($bp3list_filterfield);
    }
    function newRow() {
            log_message('debug', 'bp3list_filterfield.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3list_filterfieldid' =>  $this->input->get_post('bp3list_filterfieldid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'parentid' =>  $this->input->get_post('parentid', TRUE)
                ,'fieldsize' =>   $this->input->get_post('fieldsize', TRUE)
                ,'reftype' =>   $this->input->get_post('reftype', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'fieldtype' =>   $this->input->get_post('fieldtype', TRUE)
                ,'valuearray' =>   $this->input->get_post('valuearray', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'reftopart' =>   $this->input->get_post('reftopart', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
                $parentid =  $this->input->get_post('parentid', TRUE);
            $bp3list_filterfield= $this->m_bp3list_filterfield->newRow($instanceid,$parentid,$data);
            $return = $bp3list_filterfield;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3list_filterfield.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3list_filterfield = $this->m_bp3list_filterfield->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3list_filterfield
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
                    $bp3list_filterfield= $this->m_bp3list_filterfield->getRowsByParent($parentid,$sort);
                }else{
                    $bp3list_filterfield= $this->m_bp3list_filterfield->getRows($sort);
                }
            }else{
              $bp3list_filterfield= $this->m_bp3list_filterfield->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_filterfield
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3list_filterfield.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3list_filterfield= $this->m_bp3list_filterfield->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3list_filterfield
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
        log_message('debug', 'bp3list_filterfield.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3list_filterfield= $this->m_bp3list_filterfield->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3list_filterfield
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
        log_message('debug', 'bp3list_filterfield.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3list_filterfieldid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3list_filterfield->deleteRow($tempId);
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
        $this->load->model('M_bp3list_filterfield', 'm_bp3list_filterfield');
    }
}
?>