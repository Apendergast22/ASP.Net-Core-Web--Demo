// This error occurs when server returns response with any error.
class BadRequestError extends Error {
    constructor(message, errorJson) {
      super(message);
      this.name = "BadRequestError";
      this.errorDetails = errorJson;
    }

    // Get error details in JSON format.
    get Details(){
        return this.errorDetails ?? this.message;
    }
}

export default BadRequestError;
