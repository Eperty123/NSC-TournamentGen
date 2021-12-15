namespace NSC_TournamentGen.Dtos
{
    public class BracketDto
    {
        public int Id { get; set; }
        public int  Participant1Id { get; set; }
        public ParticipantDto  Participant1 { get; set; }
        public int  Participant2Id { get; set; }
        public ParticipantDto  Participant2 { get; set; }
        public int RoundId { get; set; }
        public RoundDto Round { get; set; }
        public int WinnerId { get; set; }
    }
}