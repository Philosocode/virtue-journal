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
import { VirtueEditPage } from './pages/virtue-edit.page';

import { EntriesPage } from "./pages/entries.page";
import { EntryDetailPage } from "./pages/entry-detail.page";
import { EntryCreatePage } from "./pages/entry-create.page";
import { EntryEditPage } from "./pages/entry-edit.page";

import { NotFoundPage } from "./pages/not-found.page";

// Other
import { store } from "./redux/store";
import { tokenIsExpired } from "./helpers/check-expired";
import { logoutUser } from './redux/auth';

// Check for token
if (localStorage.user && tokenIsExpired()) {
  // Logout user
  store.dispatch(logoutUser());

  // Redirect to login
  window.location.href = '/login';
}

export class App extends Component {
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
          <PrivateRoute exact path="/virtues/:virtueId/edit" component={VirtueEditPage} />

          {/* Need `key` so EntriesPage will re-render on route change (but same component) */}
          <PrivateRoute exact key="virtue" path="/virtues/:virtueId/entries" component={EntriesPage} />

          <PrivateRoute exact key="uncategorized" path="/entries/uncategorized" component={EntriesPage} />
          <PrivateRoute exact key="all" path="/entries/all" component={EntriesPage} />
          <PrivateRoute exact path="/entries/create" component={EntryCreatePage} />
          <PrivateRoute exact path="/entries/:entryId" component={EntryDetailPage} />
          <PrivateRoute exact path="/entries/:entryId/edit" component={EntryEditPage} />

          <PrivateRoute exact path="/settings" component={SettingsPage} />
          <Route component={NotFoundPage} />
        </Switch>
      </div>
    )
  }
};