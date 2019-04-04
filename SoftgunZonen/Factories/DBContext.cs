using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftgunZonen.Models;

namespace SoftgunZonen.Factories
{
    public class DBContext
    {
        private static volatile DBContext INSTANCE;
        public static DBContext Instance
        {
            get
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new DBContext();
                }
                return INSTANCE;
            }
        }

        private AutoFactory<Page> pageFactory;
        private AutoFactory<Category> categoryFactory;
        private AutoFactory<Product> productFactory;
        private AutoFactory<Contact> contactFactory;
        private UserFactory userFactory;
        private AutoFactory<Slider> sliderFactory;
        private MemberFactory memberFactory;
        private AutoFactory<Comment> commentFactory;
        private AutoFactory<MemberRole> memberRoleFactory;
        private AutoFactory<ContactMessage> contactMessageFactory;
        private AutoFactory<LikeRelation> likeRelationFactory;

        public AutoFactory<Page> PageFactory
        {
            get
            {
                if (pageFactory == null)
                {
                    pageFactory = new AutoFactory<Page>();
                }
                return pageFactory;
            }
        }

        public AutoFactory<Category> CategoryFactory
        {
            get
            {
                if (categoryFactory == null)
                {
                    categoryFactory = new AutoFactory<Category>();
                }
                return categoryFactory;
            }
        }

        public AutoFactory<Product> ProductFactory
        {
            get
            {
                if (productFactory == null)
                {
                    productFactory = new AutoFactory<Product>();
                }
                return productFactory;
            }
        }

        public AutoFactory<Contact> ContactFactory
        {
            get
            {
                if (contactFactory == null)
                {
                    contactFactory = new AutoFactory<Contact>();
                }
                return contactFactory;
            }
        }

        public UserFactory UserFactory
        {
            get
            {
                if (userFactory == null)
                {
                    userFactory = new UserFactory();
                }
                return userFactory;
            }
        }

        public AutoFactory<Slider> SliderFactory
        {
            get
            {
                if (sliderFactory == null)
                {
                    sliderFactory = new AutoFactory<Slider>();
                }
                return sliderFactory;
            }
        }

        public MemberFactory MemberFactory
        {
            get
            {
                if (memberFactory == null)
                {
                    memberFactory = new MemberFactory();
                }
                return memberFactory;
            }
        }

        public AutoFactory<Comment> CommentFactory
        {
            get
            {
                if (commentFactory == null)
                {
                    commentFactory = new AutoFactory<Comment>();
                }
                return commentFactory;
            }
        }

        public AutoFactory<MemberRole> MemberRoleFactory
        {
            get
            {
                if (memberRoleFactory == null)
                {
                    memberRoleFactory = new AutoFactory<MemberRole>();
                }
                return memberRoleFactory;
            }
        }

        public AutoFactory<ContactMessage> ContactMessageFactory
        {
            get
            {
                if (contactMessageFactory == null)
                {
                    contactMessageFactory = new AutoFactory<ContactMessage>();
                }
                return contactMessageFactory;
            }
        }

        public AutoFactory<LikeRelation> LikeRelationFactory
        {
            get
            {
                if (likeRelationFactory == null)
                {
                    likeRelationFactory = new AutoFactory<LikeRelation>();
                }
                return likeRelationFactory;
            }
        }
    }
}