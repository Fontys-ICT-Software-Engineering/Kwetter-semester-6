import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
    ext: {
        loadimpact: {
          projectID: 3642900,
          // Test runs with the same name groups test runs together
          name: "Load balancing Test"
        }
      },
    stages: [
        { duration: '4m', target: 250},
        { duration: '6m', target: 250},
        { duration: '4m', target: 0}
    ],
};

const API_BASE_URL = ''

export default function () {
    const urlres = http.get('http://localhost:5050/KweetRead/AllKweets')
    sleep(1)
}