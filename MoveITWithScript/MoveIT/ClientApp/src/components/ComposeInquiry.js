import React, { Component } from 'react';
import update from "lodash.set";
import authService from './api-authorization/AuthorizeService'

export class ComposeInquiry extends Component {
    static displayName = ComposeInquiry.name;

  constructor(props) {
      super(props);    
      this.handleSubmit = this.handleSubmit.bind(this);
      this.handleChange = this.handleChange.bind(this);
      this.validateForm = this.validateForm.bind(this);
      this.validateAll = this.validateAll.bind(this);
      this.handleOptionChange = this.handleOptionChange.bind(this);
      this.handleCalculatePrice = this.handleCalculatePrice.bind(this);
      this.fillUserData = this.fillUserData.bind(this);
      this.populateUserInfo = this.populateUserInfo.bind(this);

     

      this.state = {
          errorText: "",
          price: "",
          userData: {
              firstName: "",
              lastName: "",
              addressFrom: {
                  addressLine1: "",
                  addressLine2: "",
                  addressCity: ""
              }
          },
          payload: {

              firstName: "",
              lastName: "",
              addressFrom: {
                  addressLine1: "",
                  addressLine2: "",
                  addressCity: ""
              },
              addressTo: {
                  addressLine1: "",
                  addressLine2: "",
                  addressCity: ""
              },
              phone: "",
              distance: "",
              area: "",
              specialArea: "",
              options: ""
          },
          isError: {
              firstName: "",
              lastName: "",
              phone: "",
              distance: "",
              area: "",             
              addressToLine1: "",
              addressFromLine1: "",
              addressToCity: "",
              addressFromCity: "",
          },
          formValid: false,

      };
    }

    //TODO use object.keys
    validateAll = () => {

        this.validateForm("payload.firstName", this.state.firstName);
        this.validateForm("payload.lastName", this.state.lastName);
        this.validateForm("payload.addressFrom.addressLine1", this.state.payload.addressFrom.addressLine1);
        this.validateForm("payload.addressFrom.addresssCity", this.state.payload.addressFrom.addresssCity);
        this.validateForm("payload.addressTo.addressLine1", this.state.payload.addressTo.addressLine1);
        this.validateForm("payload.addressTo.addressLine1", this.state.payload.addressFrom.addressCity);
        this.validateForm("payload.distance", this.state.distance);
        this.validateForm("payload.area", this.state.area);

       
    }

    validateForm = (name, value) => {
       
        switch (name) {
            case "payload.firstName":
                this.state.isError.firstName = value.length === 0 ? "First name is required" : "";
                break;
            case "payload.firstName":
                this.state.isError.lastName = value.length === 0 ? "Last name is required" : "";
                break;
            case "payload.addressTo.addressLine1":
                this.state.isError.addressToLine1 = value.length === 0 ? "Address is required" : "";
                break;
            case "payload.addressFrom.addressLine1":
                this.state.isError.addressFromLine1 = value.length === 0 ? "Address is required" : "";
                break;
            case "payload.addressFrom.addressCity":
                this.state.isError.addressFromCity = value.length === 0 ? "City is required" : "";
                break;
            case "payload.addressTo.addressCity":
                this.state.isError.addressToCity = value.length === 0 ? "City is required" : "";
                break;
            case "payload.distance":
                if (value.length === 0) {
                    this.state.isError.distance = "Distance is required";
                }
                else {
                    if (parseInt(value) < 1 || parseInt(value) > 9999) {
                        this.state.isError.distance = "Distance should between 1 and 9999";
                    }
                    else {
                        this.state.isError.distance = "";
                    }
                }
              
                break;
            case "payload.area":
                if (value.length === 0) {
                    this.state.isError.area = "Area is required";
                }
                else {
                    if (parseInt(value) < 1 || parseInt(value) > 500) {
                        this.state.isError.area = "Area should between 5 and 500";
                    }
                    else {
                        this.state.isError.area = "";
                    }
                }
                 
                break;
            default:



        }

        //ToDo
        /*Object.keys(this.state.isError).forEach(function (key, index) {
            if (index) {
                this.setState({ formValid: false })
            }
        });*/

    }

    componentDidMount() {
        this.populateUserInfo();
    } 

    handleChange = (ev) =>
    {

        update(this.state, ev.target.name, ev.target.value);       
        this.validateForm(ev.target.name, ev.target.value);
        this.forceUpdate();

       
    }
    handleOptionChange = (ev) => {
        update(this.state, ev.target.name, ev.target.checked === true ? "Piano" : "");
        this.forceUpdate();
    }

    async populateUserInfo() {

        const token = await authService.getAccessToken();

        this.setState({ errorText: "" });

        fetch('api/user/info', {
            method: 'GET',
            headers: !token ? {} : {
                'Authorization': `Bearer ${token}`
            },           
        })
            .then(response => Promise.all([response.ok, response.json()]))
            .then(([responseOk, body]) => {
                if (responseOk) {
                    this.fillUserData(body);
                } else {
                    throw new Error(body.title);
                }
            })
            .catch(error => {
                console.error(error);
                this.setState({ errorText: error.message });
            });
    }

    async handleSubmit(event) {
        event.preventDefault();       
        const token = await authService.getAccessToken();

        this.setState({ errorText: "" });
        
        fetch('api/offer/submit', {
            method: 'POST',
            headers: !token ? {} : {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },

            body: JSON.stringify(this.state.payload)
        })
        .then(response => Promise.all([response.ok, response.json()]))
        .then(([responseOk, body]) => {
            if (responseOk) {
                this.props.history.push({
                    pathname: '/viewoffer',
                    data: body
                });
            } else {
                throw new Error(body.title);
            }
        })
        .catch(error => {
            console.error(error);
            this.setState({ errorText: error.message });
        });
    }

    async handleCalculatePrice(event) {
        event.preventDefault();

        this.validateAll();

        const token = await authService.getAccessToken();

        this.setState({ errorText: "" });

        fetch('api/offer/calculate', {
            method: 'POST',
            headers: !token ? {} : {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },

            body: JSON.stringify(this.state.payload)
        })
        .then(response => Promise.all([response.ok, response.json()]))
        .then(([responseOk, body]) => {
            if (responseOk) {
                this.setState({ price: body });
            } else {
                throw new Error(body.title);
            }
        })
        .catch(error => {
            console.error(error);
            this.setState({ errorText: error.message });
        });


      
    }

    fillUserData(userData) {

        //Todo fix with spread operator to not mutate state
        this.state.payload.firstName = userData.firstName;
        this.state.payload.lastName = userData.lastName;
        this.state.payload.addressFrom.addressLine1 = userData.addressLine1;
        this.state.payload.addressFrom.addressLine2 = userData.addressLine2;
        this.state.payload.addressFrom.addressCity = userData.city;
        this.state.payload.phone = userData.phone;
       
        this.forceUpdate();       
    }




    render() {   

        const { isError } = this.state;

      return (          

          <div class="container">
              <div class="row">
                  <div class="col-sm-10 mx-auto">
                      <form onSubmit={this.handleSubmit}>
                          <div class="form-group row">
                              <div class="col-sm-6">
                                  <label for="inputFirstname">First name</label>
                                  <input type="text" class="form-control" id="inputFirstname" name="payload.firstName" placeholder="First name" value={this.state.payload.firstName} onChange={this.handleChange} />
                                  {isError.firstName.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.firstName}</div>
                                  )}
                              </div>
                              <div class="col-sm-6">
                                  <label for="inputLastname">Last name</label>
                                  <input type="text" class="form-control" id="inputLastname" name="payload.lastName" value={this.state.payload.lastName} placeholder="Last name" onChange={this.handleChange} />
                                  {isError.lastName.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.lastName}</div>
                                  )}
                              </div>
                              <div class="col-sm-6">
                                  <label for="inputContactNumber">Contact Number</label>
                                  <input type="text" class="form-control" id="inputContactNumber" name="payload.phone" value={this.state.payload.phone} placeholder="Contact Number" onChange={this.handleChange} />
                              </div>
                          </div>
                          <div class="form-group row">
                              <div class="col-sm-6">
                                  <legend>From</legend>
                                  <label for="inputAddressFromLine1">Address</label>
                                  <input type="text" class="form-control" id="inputAddressFromLine1" name="payload.addressFrom.addressLine1" value={this.state.payload.addressFrom.addressLine1} placeholder="Street Address" onChange={this.handleChange} />
                                  {isError.addressFromLine1.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.addressFromLine1}</div>
                                  )}
                                  <label for="inputAddressFromLine2">Address (Line 2)</label>
                                  <input type="text" class="form-control" id="inputAddressFromLine2" name="payload.addressFrom.addressLine2" value={this.state.payload.addressFrom.addressLine2} placeholder="Line 2" onChange={this.handleChange}/>
                                  <label for="inputCityFrom">City</label>
                                  <input type="text" class="form-control" id="inputCityFrom" name="payload.addressFrom.addressCity" placeholder="City" value={this.state.payload.addressFrom.addressCity} onChange={this.handleChange} />
                                  {isError.addressFromCity.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.addressFromCity}</div>
                                  )}
                              </div>
                              <div class="col-sm-6">
                                  <legend>To</legend>
                                  <label for="inputAddressToLine1">Address</label>
                                  <input type="text" class="form-control" id="inputAddressToLine1" name="payload.addressTo.addressLine1" placeholder="Street Address" onChange={this.handleChange} />
                                  {isError.addressToLine1.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.addressToLine1}</div>
                                  )}
                                  <label for="inputAddressFromLine2">Address (Line 2)</label>
                                  <input type="text" class="form-control" id="inputAddressToLine2" name="payload.addressTo.addressLine2" placeholder="Line 2" onChange={this.handleChange} />
                                  <label for="inputCityTo">City</label>
                                  <input type="text" class="form-control" id="inputCityTo" name="payload.addressTo.addressCity" placeholder="City" onChange={this.handleChange} />
                                  {isError.addressToCity.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.addressToCity}</div>
                                  )}
                              </div>
                          </div>
                          <div class="form-group row">                              
                              <div class="col-sm-3">
                                  <label for="inputDistance">Distance</label>
                                  <input type="number" class="form-control" id="inputDistance" name="payload.distance" placeholder="Distance" onChange={this.handleChange} />
                                  {isError.distance.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.distance}</div>
                                  )}
                              </div>
                              <div class="col-sm-3">
                                  <label for="inputArea">Area m2</label>
                                  <input type="number" class="form-control" id="inputArea" name="payload.area" placeholder="Area" onChange={this.handleChange} />
                                  {isError.area.length > 0 && (
                                      <div class="invalid-feedback d-block">{isError.area}</div>
                                  )}
                              </div>
                              <div class="col-sm-3">
                                  <label for="inputSpecialArea">Special Area (If present)</label>
                                  <input type="number" class="form-control" id="inputSpecialArea" name="payload.specialArea" placeholder="Special Area" onChange={this.handleChange}/>
                              </div>
                              <div class="form-check col-sm-3">
                                  <label for="inputSpecialArea">Special option</label>
                                  <div class="form-check" >
                                      <input class="form-check-input" type="checkbox" id="gridCheck"  name="payload.options" onChange={this.handleOptionChange} />
                                          <label class="form-check-label" for="gridCheck">
                                               Piano
                                          </label>
                                  </div>                                
                              </div>
                             
                          </div>
                          <div class="row">
                              <div class="col-sm-12">
                                  <input type="submit" class="btn btn-primary px-4 float-right" value="Submit"  />
                                  <input type="button" class="btn btn-primary px-4 float-right" value="Get price" onClick={this.handleCalculatePrice} />
                              </div>                         
                          </div>
                      </form>
                  </div>
              </div>
              <hr/>
              <div class="row">        
                  <div class="col-sm-12">
                  {this.state.price && (
                          <div class="alert alert-primary" role="alert">
                          Price: {this.state.price} kr.
                      </div>
                  )}
                  {this.state.errorText && (
                          <div class="alert alert-danger " role="alert">
                          {this.state.errorText}
                      </div>
                      )}
                  </div>             
              </div>
          </div>
          
          
          );
    
  }
  
}
