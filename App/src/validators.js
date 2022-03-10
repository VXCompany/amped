function uriValidator () {
    return function uri (value) {
      return (value && value.startsWith('http')) || 'Please enter a valid uri'
    }
  }
  
  function requiredValidator () {
    return function required (value) {
      return (value !== undefined && value !== null && value !== '') || 'This field is required'
    }
  }
  
  export {
    uriValidator,
    requiredValidator
  }