import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class ViewOffer extends Component {
    static displayName = ViewOffer.name;

  constructor(props) {
      super(props);    
  
      this.state = {

      };
  }    
    componentDidMount() {
        if (this.props.match.params.guid) {

            fetch(`api/offer/getbylink?base64=${this.props.match.params.guid}`, {
                method: 'GET'                
            })
                .then(response => Promise.all([response.ok, response.json()]))
                .then(([responseOk, body]) => {
                    if (responseOk) {
                        this.props.location.data = body;
                        this.forceUpdate();
                    } else {
                        throw new Error(body.title);
                    }
                })
                .catch(error => {
                    console.error(error);
                    this.setState({ errorText: error.message });
                });

        }
    } 


    render() {

        if (!this.props.location.data)
            return ("No data");

      return ( 
         <div class=" container-fluid">
            <h1 id="tabelLabel">Offer {this.props.location.data.inquiry.id}</h1>
              <div class="col-sm-4">
                  <div class="row">
                      <div class="col-sm-6">
                              <h5>Offernumber: </h5> {this.props.location.data.inquiry.id}
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-sm-6">
                                  <h5>  {this.props.location.data.inquiry.firstName}  {this.props.location.data.inquiry.lastName} </h5>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-sm-6">
                                  <h5> From:  </h5>{this.props.location.data.inquiry.addressFrom.addressCity} {this.props.location.data.inquiry.addressFrom.addressLine1} {this.props.location.data.inquiry.addressFrom.addressLine2}
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-sm-6">
                                  <h5>  To:  </h5>{this.props.location.data.inquiry.addressTo.addressCity} {this.props.location.data.inquiry.addressTo.addressLine1} {this.props.location.data.inquiry.addressTo.addressLine2}
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-sm-6">
                                  <h5>   Distance: </h5>{this.props.location.data.inquiry.distance} km
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-sm-6">
                                  <h5>    Area: </h5> {this.props.location.data.inquiry.area}
                          </div>
                    </div>
                   <div class="row">
                      <div class="col-sm-6">
                          <h5> Special area:  </h5>{this.props.location.data.inquiry.specialArea}
                      </div>
                  </div>
                   <div class="row">                      
                          <div class="col-sm-6">
                              <h5> Options: </h5>  {this.props.location.data.inquiry.options}
                          </div>
                     </div>
              </div> 

              <div class="col-sm-12">
                  <div class="row">
                          <h5>Total net price </h5> {this.props.location.data.totalPriceFormatted}
                  </div>
                  <div class="row">
                          <h5> Offer date  </h5>{this.props.location.data.offerDateFormatted}
                  </div>
                  <div class="row">
                          <h5> Permanent url (no login required) </h5><a href={this.props.location.data.shortUrl}> {this.props.location.data.shortUrl}</a>
                  </div>
              </div> 

                          <Link className="text-dark" to="/composeinquiry">New Inquiry</Link>
                          
              <Link className="text-dark" to="/">Back home</Link>

          </div>   
          
          
          );
    
  }

}
