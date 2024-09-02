using Library.Data;
using Library.Models;
using Library.Models.DBObjects;

namespace Library.Repository
{
    public class MemberRepository
    {
        private ApplicationDbContext dbContext;

        public MemberRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public MemberRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> memberList = new List<MemberModel>();

            foreach (Member dbMember in dbContext.Members)
            {
                memberList.Add(MapDbObjectToModel(dbMember));
            }

            return memberList;
        }

        public MemberModel GetMemberByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Members.FirstOrDefault(x => x.Idmember == ID));
        }

        public MemberModel GetMemberByEmail(string email)
        {
	        return MapDbObjectToModel(dbContext.Members.FirstOrDefault(x => x.Email == email));
        }

        public void InsertMember(MemberModel memberModel)
        {
            memberModel.Idmember = Guid.NewGuid();

            dbContext.Members.Add(MapModelToDbObject(memberModel));
            dbContext.SaveChanges();
        }

        public void UpdateMember(MemberModel memberModel)
        {
            Member existingMember = dbContext.Members.FirstOrDefault(x => x.Idmember == memberModel.Idmember);

            if (existingMember != null)
            {
                existingMember.Idmember = memberModel.Idmember;
                existingMember.Email = memberModel.Email;
                existingMember.Adress = memberModel.Adress;
                existingMember.Phone = memberModel.Phone;
                existingMember.Name = memberModel.Name;
                dbContext.SaveChanges();
            }
        }

        public void DeleteMember(Guid id)
        {
            Member existingMember = dbContext.Members.FirstOrDefault(x => x.Idmember == id);

            if (existingMember != null)
            {
                dbContext.Members.Remove(existingMember);
                dbContext.SaveChanges();
            }
        }

        private MemberModel MapDbObjectToModel(Member dbMember)
        {
            MemberModel memberModel = new MemberModel();

            if (dbMember != null)
            {
                memberModel.Adress = dbMember.Adress;
                memberModel.Name = dbMember.Name;
                memberModel.Email = dbMember.Email;
                memberModel.Phone = dbMember.Phone;
                memberModel.Idmember = dbMember.Idmember;
            }

            return memberModel;
        }

        private Member MapModelToDbObject(MemberModel memberModel)
        {
            Member member = new Member();

            if (memberModel != null)
            {
                member.Adress = memberModel.Adress;
                member.Name = memberModel.Name;
                member.Email = memberModel.Email;
                member.Phone = memberModel.Phone;
                member.Idmember = memberModel.Idmember;
            }

            return member;
        }
    }
}