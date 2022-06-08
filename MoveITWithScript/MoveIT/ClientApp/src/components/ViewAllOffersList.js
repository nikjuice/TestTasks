import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import authService from './api-authorization/AuthorizeService'

export class ViewAllOffersList extends Component {
    static displayName = ViewAllOffersList.name;

  constructor(props) {
      super(props);    

      this.handleClick = this.handleClick.bind(this);
      this.renderOffersTable = this.renderOffersTable.bind(this);

      this.state = {
          
          offers: [],
          loading: true 

      };
  }  

    handleClick = offer => e => {
        this.props.history.push({
            pathname: '/viewoffer',
            data: offer
        });
    }

   

  componentDidMount() {
      this.populateOfferList();
    } 

     renderOffersTable(offers) {
        return (
            <div>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Offer id</th>
                        <th>Date</th>
                        <th>Price</th>
                        <th>City From</th>
                        <th>City To</th>
                    </tr>
                </thead>
                <tbody>
                        {offers.map(offer =>
                            <tr class="cursor-pointer"key={offer.offerId} onClick={this.handleClick(offer)}>
                            <td> {offer.offerId}</td>
                            <td>{offer.offerDateFormatted}</td>
                            <td>{offer.totalPriceFormatted}</td>                            
                            <td>{offer.inquiry.addressFrom.city}</td>
                            <td>{offer.inquiry.addressTo.city}</td>
                            </tr>
                            
                    )}
                </tbody>
            </table>

            <div><Link className="text-dark" to="/composeinquiry">New Inquiry</Link></div>

            <div><Link className="text-dark" to="/">Back home</Link></div>
            </div>    
        );
    }


  render() {    

      

          let contents = this.state.loading
              ? <p><em>Loading...</em></p>
              : this.renderOffersTable(this.state.offers);

             return (
          <div>
              <h1 id="tabelLabel" >Move It all offers</h1>
              <p>Here you can see all your offers</p>
              {contents}
          </div>
      );
          
          
         
    
  }

  async populateOfferList() {
    const token = await authService.getAccessToken();
      const response = await fetch('api/offer/listall', {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ offers: data, loading: false });
  }
}
