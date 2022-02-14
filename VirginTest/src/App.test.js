import { render, screen } from '@testing-library/react';
import App from './App';

test('scenario first test', () => {
    render(<App />);
    const linkElement = screen.getByText(/Scenario/i);
    expect(linkElement).toBeInTheDocument();
});
