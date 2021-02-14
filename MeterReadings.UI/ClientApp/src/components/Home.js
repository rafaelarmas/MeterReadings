import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

  constructor(props) {
      super(props);
      this.state = { numberOfSuccessfulReadings: 0, numberOfFailedReadings: 0 };
  }

  render () {
    return (
        <div>
            <form id="form1" onSubmit={this.sendData}>
                <h1>Please select CSV file to upload.</h1>
                <input type="file" id="input1" />
                <button type="submit" className="btn btn-primary btn-lg">Upload</button>
                <p aria-live="polite">Succes count: <strong>{this.state.numberOfSuccessfulReadings}</strong></p>
                <p aria-live="polite">Fail count:   <strong>{this.state.numberOfFailedReadings}</strong></p>
            </form>
        </div>
    );
  }

    async sendData() {
        const input = document.getElementById('input1');
        const data = new FormData();
        for (const file of input.files) {
            data.append('files', file, file.name);
        }
        const response = fetch('https://localhost:44359/MeterReadings/meter-reading-uploads',
            {
                //headers: {
                //    'Accept': '*/*',
                //    'Content-Type': 'multipart/form-data'
                //},
                body: data,
                method: 'POST'
            });

        response.then(results => {
            if (!results.ok) {
                console.log(results.message);
            }
            return results.json();
        });
        console.log(response);
    }
}