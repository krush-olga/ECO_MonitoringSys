import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';

import './App.css';
import { MenuView } from './components/menu/menu.jsx';
import { AdvancedMap } from './components/advancedmap/advancedmap.jsx';
import { Home } from './components/home/home.jsx';
import { MapView } from './components/map/map.jsx';
import { Dictionary } from './components/dictionary/dictionary.jsx';

import { get } from './utils/httpService';
import { ENVIRONMENTS_URL } from './utils/constants';

import {
  EnvironmentsInfoContext,
  environmentsInfoInitialState,
} from './components/context/environmentsInfoContext';
import Document from './components/document/document';

export const App = () => {
  const [user, setUser] = useState({});
  const [environmentsInfo, setEnvironmentsInfo] = useState(
    environmentsInfoInitialState
  );
  const [dictionary, setDictionary] = useState('');

  React.useEffect(() => {
    setUser(JSON.parse(sessionStorage.getItem('user')));
  }, []);

  useEffect(() => {
    get(ENVIRONMENTS_URL).then(({ data }) => {
      setEnvironmentsInfo({
        selected: null,
        environments: data,
      });
    });
  }, []);

  function redirectToExternalSite({ match }) {
    const { id } = match.params;
    window.location.href = `https://data.rada.gov.ua/laws/show/${id}`;
    return null;
  }
  return (
    <Router>
      <div className='App'>
        <EnvironmentsInfoContext.Provider
          value={{ environmentsInfo, setEnvironmentsInfo }}
        >
          <MenuView
            user={user}
            setUser={setUser}
            dictionary={dictionary}
            setDictionary={setDictionary}
          />
          <Switch>
            <Route exact path='/' component={Home} />
            <Route path='/earth' component={() => <MapView user={user} />} />
            <Route
              path='/dictionary'
              component={() => (
                <Dictionary user={user} tableName={dictionary} />
              )}
            />
            <Route
              path='/advancedmap'
              component={() => <AdvancedMap user={user} />}
            />
            <Route exact path='/document/list' component={Document} />
            <Route
              exact
              path='/laws/show/:id'
              render={redirectToExternalSite}
            />
          </Switch>
        </EnvironmentsInfoContext.Provider>
      </div>
    </Router>
  );
};
