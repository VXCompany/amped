module.exports = {
  transform: {
    '^.+\\.svelte$': 'svelte-jester',
    '^.+\\.js$': 'babel-jest',
  },
  moduleFileExtensions: ['js', 'svelte'],
  'globals': {
    'AMPED_API_URL': 'http://localhost:5001/api'
  }
}