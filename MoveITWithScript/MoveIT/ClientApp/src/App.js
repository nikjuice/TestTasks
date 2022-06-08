import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ComposeInquiry } from './components/ComposeInquiry';
import { ViewOffer } from './components/ViewOffer';
import { ViewOfferList } from './components/ViewOfferList';
import { ViewAllOffersList } from './components/ViewAllOffersList';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path="/offer/:guid" component={ViewOffer} />
            <AuthorizeRoute path='/composeinquiry' component={ComposeInquiry} />
            <AuthorizeRoute path='/viewoffer' component={ViewOffer} />
            <AuthorizeRoute path='/viewofferlist' component={ViewOfferList} />
            <AuthorizeRoute path='/viewallofferslist' component={ViewAllOffersList} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
