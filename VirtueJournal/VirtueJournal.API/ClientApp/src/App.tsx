import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';

// Components
import { Navbar } from './components/navbar.component';
import { PrivateRoute } from "./components/private-route";

// Pages
import { IndexPage } from "./pages/index.page";
import { RegisterPage } from "./pages/register.page";
import { LoginPage } from "./pages/login.page";
import { SettingsPage } from "./pages/settings.page";

import { VirtuesPage } from "./pages/virtues.page";
import { VirtueDetailPage } from "./pages/virtue-detail.page";
import { VirtueCreatePage } from './pages/virtue-create.page';

import { EntriesPage } from "./pages/entries.page";
import { EntryDetailPage } from "./pages/entry-detail.page";
import { EntryAddEditPage } from "./pages/entry-add-edit.page";

import { NotFoundPage } from "./pages/not-found.page";

class App extends Component {
  render() {
    return (
      <div className="o-site-layout">
        <Navbar />
        <Switch>
          <Route exact path="/" component={IndexPage} />
          <Route exact path="/register" component={RegisterPage} />
          <Route exact path="/login" component={LoginPage} />
          
          <PrivateRoute exact path="/virtues" component={VirtuesPage} />
          <PrivateRoute exact path="/virtues/create" component={VirtueCreatePage} />
          <PrivateRoute exact path="/virtues/:virtueId/details" component={VirtueDetailPage} />
          {/* <PrivateRoute exact path="/virtues/:virtueId/edit" component={VirtueAddEditPage} /> */}
          <PrivateRoute exact path="/virtues/:virtueId/entries" component={EntriesPage} />

          <PrivateRoute exact path="/entries/:entryId" component={EntryDetailPage} />
          <PrivateRoute exact path="/entries/:entryId/edit" component={EntryAddEditPage} />

          <PrivateRoute exact path="/settings" component={SettingsPage} />
          <Route component={NotFoundPage} />
        </Switch>
      </div>
    )
  }
};

export { App };