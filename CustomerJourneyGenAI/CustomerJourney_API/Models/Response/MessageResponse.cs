namespace CustomerJourney.API.Models.Response
{
    public class MessageResponse
    {
        //add strng.empty

        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// State = Start, InProgress, End
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// For target the right input in html client
        /// </summary>
        public string WhatAbout { get; set; } = string.Empty;

    }
}
