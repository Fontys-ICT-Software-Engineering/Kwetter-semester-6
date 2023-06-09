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
        { duration: '10s', target: 20},
        { duration: '1m', target: 20},
        { duration: '10s', target: 1000},
        { duration: '2m', target: 1000},
        { duration: '10s', target: 20},
        { duration: '1m', target: 20}
    ],
};

export default function () {

    http.batch([
        ['GET', 'http://kwetter.com/kweetread/AllKweets'],
        ['GET', 'http://kwetter.com/kweetread/reactionsbykweet?KweetID=0868032b-d764-4d5a-a123-65cbedea357b']
    ])
    sleep(1)
}