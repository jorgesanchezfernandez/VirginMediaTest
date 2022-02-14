import React from "react";
import { act } from "react-dom/test-utils";
import { render, screen } from '@testing-library/react';
import App from './App';


it("renders scenario data", async () => {
  const fakeScenarios = [{
    scenarioID: 1,
    userID: "userID",
    surname: "fakeUSer",
    forename: "fakeForename"
  }];

  jest.spyOn(global, "fetch").mockImplementation(() =>
    Promise.resolve({
      json: () => Promise.resolve(fakeScenarios)
    })
  );

  await act(async () => {
    render(<App />);
  });

  expect(document.getElementsByTagName("td")[0].textContent).toContain(fakeScenarios[0].userID);
  expect(document.getElementsByTagName("td")[1].textContent).toContain(fakeScenarios[0].surname);
  expect(document.getElementsByTagName("td")[2].textContent).toContain(fakeScenarios[0].forename);

  // remove the mock to ensure tests are completely isolated
  global.fetch.mockRestore();
});

test('scenario first test', () => {
    render(<App />);
    const linkElement = screen.getByText(/Scenario/i);
    expect(linkElement).toBeInTheDocument();
});
