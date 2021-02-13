import * as React from 'react';
import { connect } from 'react-redux';
import * as ResultsStore from '../store/Results';

type ResultsProps =
    ResultsStore.ResultsState
    & typeof ResultsStore.actionCreators;

const Home = () => (
  <div>
      <h1>Please select CSV file to upload.</h1>
        <button type="button"
            className="btn btn-primary btn-lg">
            Upload
        </button>

  </div>
);

export default connect()(Home);

