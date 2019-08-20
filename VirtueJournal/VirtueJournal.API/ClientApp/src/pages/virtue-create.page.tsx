import React, { Component } from "react";
import { Redirect } from 'react-router';
import { connect } from 'react-redux';

import { createVirtue } from "../redux/virtue";
import { AxiosError, AxiosResponse } from "axios";

interface Props {
  createVirtue: Function
}

class _VirtueCreatePage extends Component<Props> {
  state = {
    color: "",
    description: "",
    icon: "",
    name: "",
    error: "",
    shouldRedirectToVirtuesPage: false
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  };

  handleSubmit = (event: React.SyntheticEvent) => {
    event.preventDefault();

    const { color, description, icon, name } = this.state;
    const virtueToCreate = { color, description, icon, name };

    this.props.createVirtue(virtueToCreate)
      .then((res: AxiosResponse) => {
        this.setState({ shouldRedirectToVirtuesPage: true });
      })
      // .then(() => this.setState({ shouldRedirectToVirtuesPage: true }))
      .catch((err: AxiosError) => {
        const errorResponse = err.response;

        if (errorResponse) {
          this.setState({ errorMessage: `Error: ${errorResponse.data}` });
        }
      });
  }

  render() {
    return (
      <div>
        { this.state.shouldRedirectToVirtuesPage && <Redirect to="/virtues" /> }
        <h1>Virtue Create Page</h1>
        <form onSubmit={this.handleSubmit}>

          <div className="form__group">
            <label htmlFor="name" className="form__label">Name:</label>
            <div className="form__input-container">
              <input
                required
                id="name"
                className="form__input"
                type="text"
                name="name"
                placeholder="Name"
                value={this.state.name}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="name" className="form__label">Color:</label>
            <div className="form__input-container">
              <input
                required
                id="color"
                className="form__input"
                type="text"
                name="color"
                placeholder="Color"
                value={this.state.color}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="icon" className="form__label">Icon:</label>
            <div className="form__input-container">
              <input
                required
                id="icon"
                className="form__input"
                type="text"
                name="icon"
                placeholder="Icon"
                value={this.state.icon}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <div className="form__group">
            <label htmlFor="description" className="form__label">Description</label>
            <div className="form__input-container">
              <textarea
                id="description"
                className="form__input"
                name="description"
                placeholder="Description"
                value={this.state.description}
                onChange={this.handleChange}
              />
            </div>
          </div>

          <button className="button" type="submit">Create</button>
          <div className="error error--server">{this.state.error}</div>
        </form>
      </div>
    )
  }
}

export const VirtueCreatePage = connect(
  null, 
  { createVirtue }
)(_VirtueCreatePage);