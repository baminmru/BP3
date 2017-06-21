﻿<?php
	 class C_bp3report_filter extends CI_Controller {
    function __construct() {
         parent::__construct();
        $this->_loadModels();
    }
    function setRow() {
          log_message('debug', 'bp3report_filter.setRow post : '.json_encode($this->input->post(NULL, FALSE)));
          log_message('debug', 'bp3report_filter.getRows get : '.json_encode($this->input->get(NULL, FALSE)));
          $data = array(
                'bp3report_filterid' =>  $this->input->get_post('bp3report_filterid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'allowignore' =>   $this->input->get_post('allowignore', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
            );
            $bp3report_filter = $this->m_bp3report_filter->setRow($data);
            print json_encode($bp3report_filter);
    }
    function newRow() {
            log_message('debug', 'bp3report_filter.newRow post : '.json_encode($this->input->post(NULL, FALSE)));
          $data = array(
                'bp3report_filterid' =>  $this->input->get_post('bp3report_filterid', TRUE)
                ,'instanceid' =>  $this->input->get_post('instanceid', TRUE)
                ,'name' =>   $this->input->get_post('name', TRUE)
                ,'sequence' =>   $this->input->get_post('sequence', TRUE)
                ,'allowignore' =>   $this->input->get_post('allowignore', TRUE)
                ,'caption' =>   $this->input->get_post('caption', TRUE)
            );
                $instanceid =  $this->input->get_post('instanceid', TRUE);
            $bp3report_filter= $this->m_bp3report_filter->newRow($instanceid,$data);
            $return = $bp3report_filter;
            print json_encode($return);
    }
    function getRow() {
        log_message('debug', 'bp3report_filter.getRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $empId  =  $this->input->get_post('id', TRUE);
        if (isset($empId)) {
            $bp3report_filter = $this->m_bp3report_filter->getRow($empId);
            $return = array(
                'success' => true,
                'data'    => $bp3report_filter
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
            $instanceid=$this->input->get('instanceid', FALSE);
            if(isset($instanceid)){
                if($instanceid!=''){
                    $bp3report_filter= $this->m_bp3report_filter->getRowsByInstance($instanceid,$sort);
                }else{
                    $bp3report_filter= $this->m_bp3report_filter->getRows($sort);
                }
            }else{
              $bp3report_filter= $this->m_bp3report_filter->getRows($sort);
            }
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_filter
            );
        print json_encode($return);
    }
    function getRowsByInstance() {
        log_message('debug', 'bp3report_filter.getRowsByInstance post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3report_filter= $this->m_bp3report_filter->getRowsByInstance($InstId,$sort);
            $return = array(
                'success' =>  TRUE ,
                'data'    => $bp3report_filter
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
        log_message('debug', 'bp3report_filter.getRowsByParent post : '.json_encode($this->input->post(NULL, FALSE)));
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
            $bp3report_filter= $this->m_bp3report_filter->getRowsByParent($ParentId,$sort);
            $return = array(
                'success' => TRUE,
                'data'    => $bp3report_filter
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
        log_message('debug', 'bp3report_filter.deleteRow post : '.json_encode($this->input->post(NULL, FALSE)));
        $tempId  =  $this->input->get_post('bp3report_filterid', TRUE);
        if (strlen($tempId) > 0) {
            $return = $this->m_bp3report_filter->deleteRow($tempId);
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
        $this->load->model('M_bp3report_filter', 'm_bp3report_filter');
    }
}
?>