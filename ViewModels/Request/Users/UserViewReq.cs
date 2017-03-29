using System.Collections.Generic;

namespace SimpleInvoices.ViewModels{
    public class UserViewReq{
        public UserViewReq(){
            customFields=new List<CustomFieldRes>();
        }
        public int id {get;set;}
        public string name {get;set;}
        public string address {get;set;}
        public string contact {get;set;}
        public string email {get;set;}
         public string sholder {get;set;}
    public string chest{get;set;}
    public string upperWaist{get;set;}
    public string waist{get;set;}
    public string lowerWaist{get;set;}
    public string hips{get;set;}
    public string armHole{get;set;}
    public string fullSleveLength{get;set;}
    public string sleeveLength{get;set;}
    public string bicep {get;set;}
    public string forearm{get;set;}
    public string wrist {get;set;}
    public string longShirtLength{get;set;}
    public string shortShirtLength{get;set;}
    public string chaak{get;set;}
    public string daaman{get;set;}
    public string frontNeckDepth {get;set;}
    public string frontNeckWidth{get;set;}
    public string backNeckDepth{get;set;}
    public string backNeckWidth{get;set;}
    public string thigh {get;set;}
    public string kneeCap{get;set;}
    public string calf{get;set;}
    public string ankle {get;set;}
    public string pantLength{get;set;}
    public string measurementType{get;set;}
    public string imagepath {get;set;}
    public List<CustomFieldRes> customFields {get;set;}
        
    }
}